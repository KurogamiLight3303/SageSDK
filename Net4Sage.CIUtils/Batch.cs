using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage;
using Net4Sage.DataAccessModel;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Windows.Forms;
using ReportEngine;

namespace Net4Sage.CIUtils
{
    public abstract class Batch
    {
        #region "Internal Data"
        protected CommonRegistrationForm RegistrationForm { get; private set; }
        protected Context _context { get; set; }
        protected SageSession SysSession { get; private set; }
        protected bool ShowUI { get; private set; }
        protected bool ShowReports { get; private set; }
        protected bool CanBeRegistred { get => this.BatchStatus == 4 || this.BatchStatus == 5; }
        #endregion

        #region "Options Data"
        protected int UnitPriceDecimals { get; set; }
        protected int QtyDecimals { get; set; }
        #endregion

        #region "Batch Data"
        public int BatchKey { get; protected set; }
        public DateTime? PostDate { get; protected set; }
        public int BatchNo { get; protected set; }
        public string BatchID { get; protected set; }
        public BatchTypes BatchType { get; protected set; }
        public int BatchStatus { get; set; }
        #endregion

        #region "Batch Methods"
        protected void LoadContext()
        {
            _context = SysSession.CreateDBContext<Context>();
        }
        private static OperationResult GetNextBatchNo(ref SageSession session, short module, BatchTypes type, string Cmnt, DateTime postDate, out int batchKey, out int batchNo)
        {
            SqlCommand cmd;
            int retVal = 0;
            batchKey = 0;
            batchNo = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spGetNextBatch", session.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_iCompanyID", session.CompanyID);
                cmd.Parameters.AddWithValue("@_iModuleNo", module);
                cmd.Parameters.AddWithValue("@_iBatchType", (int)type);
                cmd.Parameters.AddWithValue("@_iUserID", session.UserID);
                cmd.Parameters.AddWithValue("@_iDefBatchCmnt", Cmnt);
                cmd.Parameters.AddWithValue("@_iPostDate", postDate);
                cmd.Parameters.Add(new SqlParameter("@_oBatchKey", batchKey)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.Parameters.Add(new SqlParameter("@_oNextBatchNo", batchNo)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.Parameters.Add(new SqlParameter("@_oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int16,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.Parameters.AddWithValue("@_optHiddenBatch", null);
                cmd.Parameters.AddWithValue("@_optInvcDate", null);
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                session.WriteLog(exc, "Getting Next Batch No");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null && cmd.Parameters[8].Value != null)
                {
                    switch (int.Parse(cmd.Parameters[8].Value.ToString()))
                    {
                        case 1:
                            try
                            {
                                batchNo = int.Parse(cmd.Parameters[7].Value.ToString());
                                batchKey = int.Parse(cmd.Parameters[6].Value.ToString());
                                result = new OperationResult(true);
                            }
                            catch (Exception exc)
                            {
                                session.WriteLog(exc, "Getting Next Batch No");
                                result = new OperationResult(false, exc.Message);
                            }
                            break;
                        default:
                            result = new OperationResult(false, "Error" + (int)cmd.Parameters[8].Value);
                            break;
                    }
                }
                cmd.Dispose();
            }

            return result;
        }
        public static short GetBatchModule(BatchTypes type)
        {
            switch (type)
            {
                case BatchTypes.GeneralJournals:
                    return 3;
                case BatchTypes.PaymentInvoiceAppls:
                    return 5;
            }
            throw new Exception("Batch Not Defined");
        }
        public OperationResult UpdateBatchStatus()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spGetBatchStatus", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.Add(new SqlParameter("@oStatus", retVal)
                {
                    DbType = System.Data.DbType.Int16,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int16,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Updating Batch Status");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if (cmd.Parameters[2].Value != null && int.Parse(cmd.Parameters[1].Value.ToString()) <= 0)
                        result = new OperationResult(false, "Error");
                    else
                    {
                        result = new OperationResult(true);
                        BatchStatus = int.Parse(cmd.Parameters[1].Value.ToString());
                    }
                }
                cmd.Dispose();
            }
            return result;
        }
        public virtual OperationResult DeleteBatch()
        {
            LoadContext();
            BatchLog log;
            if ((log = _context.BatchLogs.Where(p => p.BatchKey == this.BatchKey).FirstOrDefault()) == null)
                return new OperationResult(false, "Batch not found");
            if(log.Status == 6)
                return new OperationResult(false, "Batch Posted");
            _context.BatchLogs.DeleteObject(log);
            try
            {
                _context.SaveChanges();
                return new OperationResult(true);
            }catch(Exception exc)
            {
                SysSession.WriteLog(exc, "Deleting Batch");
                return new OperationResult(false, exc.Message);
            }
        }
        #endregion

        #region "Create Methods"
        protected Batch(SageSession session, int BatchKey)
        {
            BatchLog batch;
            this.BatchKey = BatchKey;
            this.SysSession = session;
            LoadContext();
            if ((batch = _context.BatchLogs.Where(p => p.BatchKey == BatchKey).FirstOrDefault()) == null)
                throw new ArgumentException();

            this.PostDate = batch.PostDate;
            this.BatchType = (BatchTypes)batch.BatchType;
            this.BatchID = batch.BatchID;
            this.BatchNo = batch.BatchNo;
            this.BatchStatus = batch.Status;
        }
        protected static int Create(SageSession session, BatchTypes type, DateTime? postDate, string Cmnt = "")
        {
            if (postDate == null)
                postDate = session.BusinessDate;
            int key , no ;
            short module = GetBatchModule(type);
            OperationResult result = GetNextBatchNo(ref session, module, type, Cmnt, postDate.Value, out key, out no);
            if (result.Success)
                return key;
            return 0;
        }
        #endregion

        #region "Register Methods"
        private OperationResult StartProcess()
        {
            OperationResult result;
            if (!(result = UpdateBatchStatus()).Success)
            {
                if(ShowUI)
                    MessageBox.Show("Error: " + result.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }
            if (!CanBeRegistred)
            {
                if (ShowUI)
                    MessageBox.Show("El lote no puede ser registrado o aplicado", "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }
            result = PreProcess();
            if (!result.Success)
            {
                if (ShowUI)
                    MessageBox.Show("Error: " + result.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return result;
            }
            if (!ShowUI || RegistrationForm.DoPost)
            {
                if (!ShowUI || !RegistrationForm.DoConfirmPost || MessageBox.Show("Desea continuar la applicación", "Sage MAS 500", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if(!ShowUI && ShowReports)
                        if(MessageBox.Show("Desea continuar la applicación", "Sage MAS 500", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        {
                            CleanRegistration();
                            return new OperationResult(false, "Cancel by User");
                        }
                    if (!IsRegisterPrinted())
                        return new OperationResult(false, "Not Registered");

                    result = PostBatch();
                    if (!result.Success)
                    {
                        if (ShowUI)
                            MessageBox.Show("Error: " + result.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return result;
                    }
                    if (ShowUI)
                    {
                        MessageBox.Show("Applicación Completada con éxito", "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        RegistrationForm.DisableAll();
                    }
                }
                else
                    CleanRegistration();
            }
            return new OperationResult(true);
        }
        protected virtual OperationResult CleanRegistration()
        {
            OperationResult answer;
            SqlCommand cmd = new SqlCommand("delete from tciErrorLog where BatchKey = " + BatchKey, SysSession.GetConnection());
            try
            {
                cmd.ExecuteNonQuery();
                answer = new OperationResult(true);
            }
            catch(Exception exc){
                SysSession.WriteLog(exc, "Cleaning Registration");
                answer = new OperationResult(false, "Error cleaning Errors");
            }
            finally
            {
                cmd.Dispose();
            }
            return answer;
        }
        public void ShowRegisterPost()
        {
            this.ShowUI = true;
            OperationResult result;
            if (!(result = UpdateBatchStatus()).Success)
            {
                MessageBox.Show("Error: " + result.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!CanBeRegistred)
            {
                MessageBox.Show("El lote no puede ser registrado o aplicado", "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            RegistrationForm = new CommonRegistrationForm(SysSession);
            InitializateRegistration();
            RegistrationForm.MainMenu.OnSave += delegate
            {
                StartProcess();
            };
            RegistrationForm.ShowDialog();
        }
        public OperationResult RegisterPost(bool showReports = false)
        {
            OperationResult result;
            this.ShowUI = false;
            this.ShowReports = showReports;
            if (!(result = UpdateBatchStatus()).Success)
                return result;
            if (!CanBeRegistred)
                return new OperationResult(false, "El lote no puede ser registrado o aplicado");
            InitializateRegistration();
            return StartProcess();
        }
        protected virtual void InitializateRegistration()
        {
            LoadContext();
        }
        private OperationResult PreProcess()
        {
            bool HaveWarnings = false;
            IEnumerable<ErrorLog> Warnings;
            OperationResult result;
            if (!ShowUI || RegistrationForm.DoRegister)
            {
                if (!(result = CleanRegistration()).Success)
                    return result;

                if (!(result = CheckPeriod()).Success)
                    return result;

                if (!(result = ValidateData()).Success)
                    return result;

                if ((Warnings = PostErrorsCount()).Any())
                {
                    if (!Warnings.Where(p => p.Severity == 2).Any())
                        HaveWarnings = true;
                    else
                    {
                        ShowErrorLogs(Warnings);
                        return new OperationResult(false);
                    }
                }
                    

                if (!(result = PrePost()).Success)
                    return result;

                if ((Warnings = PostErrorsCount()).Any())
                {
                    if (!Warnings.Where(p => p.Severity == 2).Any())
                        HaveWarnings = true;
                    else
                    {
                        ShowErrorLogs(Warnings);
                        return new OperationResult(false);
                    }
                }

                if (!(result = ValidatePostingCurrencies()).Success)
                    return result;

                if ((Warnings = PostErrorsCount()).Any())
                {
                    if (!Warnings.Where(p => p.Severity == 2).Any())
                        HaveWarnings = true;
                    else
                    {
                        ShowErrorLogs(Warnings);
                        return new OperationResult(false);
                    }
                }

                if (!(result = CheckBalance()).Success)
                    return result;

                if ((Warnings = PostErrorsCount()).Any())
                {
                    if (!Warnings.Where(p => p.Severity == 2).Any())
                        HaveWarnings = true;
                    else
                    {
                        ShowErrorLogs(Warnings);
                        return new OperationResult(false);
                    }
                }

                if (!(result = CheckRegPrintFlag()).Success)
                    return result;

                if(ShowUI || ShowReports)
                    ShowRegistrationReports();
                if (HaveWarnings)
                    ShowErrorLogs(Warnings);
            }

            return new OperationResult(true);
        }
        protected virtual OperationResult PostBatch()
        {
            OperationResult result;
            if (!(result = CheckRegisterStatus()).Success)
                return result;
            if (GetBatchModule(BatchType) == 4 || GetBatchModule(BatchType) == 5)
            {
                if (!(result = ModulePost()).Success)
                    return result;
                if (!(result = GLPost()).Success)
                    return result;
                if (!(result = CheckRegisterStatus()).Success)
                    return result;
                if (!(result = ModuleCleanUp()).Success)
                    return result;
            }
            return new OperationResult(true);
        }
        private OperationResult CheckPeriod()
        {
            FiscalPeriod period = _context.FiscalPeriods.Where(p => p.CompanyID == SysSession.CompanyID && p.StartDate <= PostDate && p.EndDate >= PostDate).FirstOrDefault();
            if (period == null)
                return new OperationResult(false, "Fiscal Period do not exist");

            if(period.Status == 2)
                return new OperationResult(false, "Fiscal Period is closed");

            return new OperationResult(true);
        }
        private OperationResult ValidateData()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spciValidatePosting", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iSessionID", 1);
                cmd.Parameters.AddWithValue("@iSourceModule", GetBatchModule(BatchType));
                cmd.Parameters.AddWithValue("@iBatchType", (int)BatchType);
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Validing Data for Post");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null && cmd.Parameters[4].Value != null)
                {
                    switch (int.Parse(cmd.Parameters[4].Value.ToString()))
                    {
                        case -1:
                        case 2:
                        default:
                            result = new OperationResult(false, "Error");
                            break;
                        case 1:
                            result = new OperationResult(true, "Warnings Detected");
                            break;
                        case 0:
                            result = new OperationResult(true); 
                            break;
                    }
                }
                cmd.Dispose();
            }
            return result;
        }
        protected virtual OperationResult PrePost()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spciPrePost", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iSessionID", 1);
                cmd.Parameters.AddWithValue("@iSourceModule", GetBatchModule(BatchType));
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "CI Prepost");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if(cmd.Parameters[3].Value != null && int.Parse(cmd.Parameters[3].Value.ToString()) != 0)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true); ;
                }   
                cmd.Dispose();
            }
            return result;
        }
        protected virtual OperationResult ValidatePostingCurrencies()
        {
            return new OperationResult(true);
        }
        protected virtual OperationResult CheckBalance()
        {
            return new OperationResult(true);
        }
        protected virtual void ShowRegistrationReports()
        {
            IEnumerable<GLPostingReportEntry> postings = SysSession.LookupObjectList<GLPostingReportEntry>("select p.CurrID as Currency," +
                " s.GLAcctNo as Account, s.Description, p.ExtCmnt as JournalDescription, p.PostAmt, p.PostAmtHC, p.PostDate as Date, (t.JrnlID + '-' + CONVERT(varchar(8), p.JrnlNo)) as JournalID," +
                " t.Description as JournalDescription , f.BatchID, isnull(f.BatchCmnt, '') as BatchDescription, f.PostDate from tglPosting as p" +
                " join tglAccount as s on p.GLAcctKey = s.GLAcctKey join tglJournal as t on t.JrnlKey = p.JrnlKey join tciBatchLog as f" +
                " on f.BatchKey = p.BatchKey where p.BatchKey = " + BatchKey).ToList();
            GLPostingReport report = new GLPostingReport();
            report.SetDataSource(postings);
            CrystalReportEngine reporter = new CrystalReportEngine();
            reporter.SysSession = SysSession;
            reporter.Report = report;
            reporter.Show();
        }

        private void ShowErrorLogs(IEnumerable<ErrorLog> logs)
        {
            ErrorLogReport report = new ErrorLogReport();
            report.SetDataSource(logs);
            CrystalReportEngine reporter = new CrystalReportEngine();
            reporter.SysSession = SysSession;
            reporter.Report = report;
            reporter.Show();
        }
        #endregion

        #region "Post Methods"
        private OperationResult CheckRegPrintFlag()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("sparSetRegPrintFlg", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iFlagValue", 1);
                cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                cmd.Parameters.Add(new SqlParameter("@iRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Seting Print Flag");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if (cmd.Parameters[3].Value != null && int.Parse(cmd.Parameters[3].Value.ToString()) != 0)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true); ;
                }
                cmd.Dispose();
            }
            return result;
        }
        private OperationResult ModulePost()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spciModPost", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iSessionID", 1);
                cmd.Parameters.AddWithValue("@iSourceModule", GetBatchModule(BatchType));
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "CI Module Post");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if (cmd.Parameters[3].Value != null && int.Parse(cmd.Parameters[3].Value.ToString()) != 0)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true); ;
                }
                cmd.Dispose();
            }
            return result;
        }
        private OperationResult GLPost()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spciGLPost", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iSourceModule", GetBatchModule(BatchType));
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "GL Post");
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
        private OperationResult CheckRegisterStatus()
        {
            SqlCommand cmd;
            int printStatus = 0, errorStatus = 0, status = 0, recordExist = 0;
             OperationResult result = null;
            cmd = new SqlCommand("sparCheckRgstrStatus2", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iBatchType", BatchType);
                cmd.Parameters.Add(new SqlParameter("@oPrintStatus", printStatus)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.Parameters.Add(new SqlParameter("@oErrorStatus", errorStatus)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.Parameters.Add(new SqlParameter("@oStatus", status)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.Parameters.Add(new SqlParameter("@oRecordsExist", recordExist)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Checking Registration Status");
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if (cmd.Parameters[3].Value != null && int.Parse(cmd.Parameters[3].Value.ToString()) != 1)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true); ;
                }
                cmd.Dispose();
            }
            return result;
        }
        private OperationResult ModuleCleanUp()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spciModCleanup", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@iSourceModule", GetBatchModule(BatchType));
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc, "Module CleanUp");
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
        private IEnumerable<ErrorLog> PostErrorsCount()
        {
            return SysSession.LookupObjectList<ErrorLog>("*", "tciErrorLog", "BatchKey =" + BatchKey);
        }
        private bool IsRegisterPrinted()
        {
            object sender = SysSession.Lookup("RgstrPrinted", "tciBatchLog", "BatchKey =" + BatchKey);
            if (sender != null)
                return true;
            return false;
        }
        #endregion
    }
}
