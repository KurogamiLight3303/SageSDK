using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage.CIUtils;
using Net4Sage.DataAccessModel;

namespace Net4Sage.GLUtils
{
    public class GeneralJournalsBatch : Batch
    {
        private List<JournalAnnotation> _annotations;

        public GeneralJournalsBatch(SageSession session, int BatchKey) : base(session, BatchKey)
        {
            _annotations = new List<JournalAnnotation>();
            if (PostDate == null)
                PostDate = _context.GLBatches.Where(p => p.BatchKey == BatchKey).Select(p => p.PostDate).FirstOrDefault();
        }

        public OperationResult AddAnnotation(JournalAnnotation journal, IEnumerable<JournalAnnotationReference> references = null)
        {
            if (journal.Currency == SysSession.HomeCurrID)
                journal.ExchangeRate = 1;
            else if (SysSession.Lookup<string>("CurrID", "tmcCurrency", "CurrID = " + SageSession.QuoteString(journal.Currency)) != null)
                return new OperationResult(false, "Currency not found");
            if (references != null)
                journal.AddRange(references);
            if (!journal.IsBalance)
                return new OperationResult(false, "Out of Balance");
            _annotations.Add(journal);
            return new OperationResult(true);
        }

        public OperationResult Save()
        {
            int? key = 0, jounalNo = 0, retVal = 0, journalKey, acctKey;
            JournalHdr hdr;
            JournalDetail line;
            LoadContext();
            foreach (JournalAnnotation i in _annotations)
            {
                if ((journalKey = _context.Journals.Where(p => p.JrnlID == i.Journal && p.CompanyID == SysSession.CompanyID).Select(p => p.JrnlKey).FirstOrDefault()) == 0)
                    return new OperationResult(false, "Journal " + i.Journal + " not found");
                if (i.JournalNumber == 0)
                {
                    _context.GetNextSurrogateKey("tglJournalHdr", ref key);
                    if (key == 0)
                        return new OperationResult(false, "Error loading surrogate key for tglJournalHdr");
                    _context.GiveNextJrnlNo(SysSession.CompanyID, journalKey, 1, ref jounalNo, ref retVal);
                    i.JournalNumber = jounalNo.Value;
                    hdr = new JournalHdr()
                    {
                        BatchKey = this.BatchKey,
                        JrnlHdrKey = key.Value,
                        CheckNo = 0,
                        CreateType = 0,
                        CompanyID = SysSession.CompanyID,
                        OffsetPost = i.Type,
                        JrnlKey = journalKey.Value,
                        JrnlNo = i.JournalNumber,
                        CurrID = i.Currency,
                        CurrExchRate = (double)i.ExchangeRate,
                        Financial = (short)(i.Financial ? 1 : 0),
                        GLAcctKey = null,
                        NextEntryNo = i.NextEntryNo,
                        OutOfBalance = (short)(i.IsBalance ? 0 : 1),
                        OutOfBalOverride = 0,
                        ReqOffsetAmt = 0,
                        ReverseJrnl = 0,
                        SourceModule = "GL",
                        Status = i.IsBalance ? JournalStatus.Balance : JournalStatus.Desbalance,
                        CreateDate = DateTime.Now,
                        CreateUserID = SysSession.UserID,
                    };
                    try
                    {
                        _context.JournalLogs.AddObject(new JournalLog()
                        {
                            CompanyID = SysSession.CompanyID,
                            JrnlKey = journalKey.Value,
                            JrnlNo = i.JournalNumber,
                            PostDate = PostDate.Value,
                            TranStatus = 2,
                        });
                        _context.SaveChanges();
                    }
                    catch (Exception exc)
                    {
                        SysSession.WriteLog(exc, "Saving Batch");
                        return new OperationResult(false, exc.Message);
                    }
                    try
                    {
                        _context.JournalHdrs.AddObject(hdr);
                        _context.SaveChanges();
                    }
                    catch (Exception exc)
                    {
                        SysSession.WriteLog(exc, "Saving Batch");
                        return new OperationResult(false, exc.Message);
                    }
                }
                else if ((hdr = _context.JournalHdrs.Where(p => p.JrnlKey == journalKey && p.JrnlNo == i.JournalNumber).FirstOrDefault()) == null)
                    return new OperationResult(false, "Journal " + i.JournalNumber + "-" + i.Journal + " not found");
                    
                foreach(JournalAnnotationReference j in i.GetAnnotationReferences())
                {

                    if ((acctKey = _context.GLAccounts.Where(p => p.CompanyID == SysSession.CompanyID && p.GLAcctNo == j.AccountNumber).Select(p => p.GLAcctKey).FirstOrDefault()) == null)
                        return new OperationResult(false, "Account not found: " + j.AccountNumber);
                    
                    if ((line = hdr.Details.Where(p => p.EntryNo == j.No).FirstOrDefault()) == null)
                    {
                        line = new JournalDetail()
                        {
                            EntryNo = j.No,
                            GLAcctKey = acctKey,
                            PostAmt = j.PostAmt,
                            PostAmtHC = j.PostAmtHC,
                            SeqNo = 16384 * j.No,
                            PostQty = 0,
                            PostCmnt = j.Comment,
                            RefNo = "",
                            TargetCompanyID = SysSession.CompanyID,
                            TranDate = i.Date
                        };
                        hdr.Details.Add(line);
                    }
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception exc)
                {
                    SysSession.WriteLog(exc, "Saving Batch");
                    return new OperationResult(false, exc.Message);
                }
            }
            return new OperationResult(true);
        }

        public static GeneralJournalsBatch CreateJournalBatch(SageSession session, DateTime? PostDate, string Cmnt = "")
        {
            BatchLog log;
            int Key = Create(session, BatchTypes.GeneralJournals, PostDate, Cmnt);
            Context context = session.CreateDBContext<Context>();
            if (Key > 0 && (log = context.BatchLogs.Where(p => p.BatchKey == Key).FirstOrDefault()) != null)
            {
                context.GLBatches.AddObject(new GLBatch()
                {
                    BatchKey = Key,
                    BatchCmnt = Cmnt,
                    GeneralJrnlType = GeneralJournalType.Transaction,
                    Hold = 0,
                    InterCompany = 0,
                    InterCompUsage = 0,
                    OrigUserID = session.UserID,
                    PostDate = PostDate.Value,
                    Private = 0,
                    TranCtrl = 0,
                    UpdateCounter = 0
                });
                //Fix error in GL Batches
                log.PostDate = PostDate;
                try
                {
                    context.SaveChanges();
                    return new GeneralJournalsBatch(session, Key);
                }
                catch(Exception exc)
                {
                    session.WriteLog(exc);
                }
            }
            return null;
        }

        protected override OperationResult PrePost()
        {
            OperationResult answer;
            if (!(answer = base.PrePost()).Success)
                return answer;

            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spglJournalRgstr", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iSourceCompanyID", SysSession.CompanyID);
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iModuleID", "GL");
                cmd.Parameters.AddWithValue("@iModuleNo", GetBatchModule(BatchType));
                cmd.Parameters.AddWithValue("@iUserID", SysSession.UserID);
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Journal Registration");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if (cmd.Parameters[5].Value != null && int.Parse(cmd.Parameters[5].Value.ToString()) != 0)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true); ;
                }
                cmd.Dispose();
            }
            return result;
        }

        protected override OperationResult CleanRegistration()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = base.CleanRegistration();
            if (!result.Success) return result;
            cmd = new SqlCommand("spglJournalRgstrDelete", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Journal Clean");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if (cmd.Parameters[2].Value != null && int.Parse(cmd.Parameters[2].Value.ToString()) != 0)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true); ;
                }
                cmd.Dispose();
            }
            return result;
        }

        protected override OperationResult PostBatch()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spglGeneralJrnlPostAll", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iFullGL", 0);
                cmd.Parameters.AddWithValue("@iCommitFlag", 1);
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Post Batch");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null && cmd.Parameters[4].Value != null)
                {
                    switch (int.Parse(cmd.Parameters[4].Value.ToString()))
                    {
                        case 2:
                            result = new OperationResult(false, "Retained Earnings Account doesn't exist");
                            break;
                        case 1:
                            result = new OperationResult(true);
                            break;
                        case 0:
                            result = new OperationResult(false,"Unsuccessfull") ;
                            break;
                        case 3:
                            result = new OperationResult(false, "Period is closed");
                            break;
                        default:
                            result = new OperationResult(false, "Other Errors");
                            break;
                    }
                }
                cmd.Dispose();
            }
            return result;
        }

        public override OperationResult DeleteBatch()
        {
            SqlCommand cmd;
            GLBatch batch;
            List<JournalHdr> hdr = _context.JournalHdrs.Where(p => p.BatchKey == this.BatchKey).ToList();
            for (var i = 0; i < hdr.Count; i++)
            {
                try
                {
                    _context.JournalHdrs.DeleteObject(hdr[i]);
                    cmd = new SqlCommand("delete from tglJournalDetl where JrnlHdrKey = " + hdr[i].JrnlHdrKey, SysSession.GetConnection());
                    cmd.ExecuteNonQuery();
                    _context.SaveChanges();
                }
                catch(Exception exc)
                {
                    SysSession.WriteLog(exc, "Deleting Batch");
                    return new OperationResult(false, exc.Message);
                }
            }
            if((batch = _context.GLBatches.Where(p => p.BatchKey == BatchKey).FirstOrDefault())!= null)
            {
                try
                {
                    _context.GLBatches.DeleteObject(batch);
                    _context.SaveChanges();
                }
                catch (Exception exc)
                {
                    SysSession.WriteLog(exc, "Deleting Batch");
                    return new OperationResult(false, exc.Message);
                }
            }
            return base.DeleteBatch();
        }
    }
}
