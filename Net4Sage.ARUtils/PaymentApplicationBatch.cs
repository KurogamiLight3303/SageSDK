using Net4Sage.CIUtils;
using Net4Sage.DataAccessModel;
using ReportEngine;
using System;
using System.Collections.Generic;
using System.Data.EntityClient;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.ARUtils
{
    public class PaymentApplicationBatch : Batch
    {
        private bool TrackTaxOnSales { get; set; }
        private bool UseMultiCurrency { get; set; }
        private bool IntegratedWithCM { get; set; }

        public PaymentApplicationBatch(SageSession session, int BatchKey) : base(session, BatchKey)
        {
        }

        public OperationResult AddPaymentAppl(int PaymentKey, IEnumerable<Tuple<int, decimal>> InvoicesAppls)
        {
            Invoice invoice;
            CustomerPayment payment;
            List<PendingCustomerPaymentAppl> pendings = new List<PendingCustomerPaymentAppl>();
            int seqNo;
            decimal pendingAmt = 0;
            BatchLog log;
            LoadContext();
            if ((log = _context.BatchLogs.Where(p => p.BatchKey == BatchKey).FirstOrDefault()) == null)
                return new OperationResult(false, "Batch Log not found");

            seqNo = log.NextSeqNo;

            if ((payment = _context.CustomerPayments.Where(p => p.CustPmtKey == PaymentKey).FirstOrDefault()) == null)
            {
                SysSession.WriteLog("Payment not found with key " + PaymentKey);
                return new OperationResult(false, "Payment not found");
            }

            pendingAmt = _context.PendingCustomerPaymentAppls.Where(p => p.ApplyFromPmtKey == PaymentKey).ToList().Sum(p => p.PmtAmtIC);

            if (payment.UnappliedAmtHC < InvoicesAppls.Sum(p => Math.Round(p.Item2 * (decimal)payment.CurrExchRate, 2)) + pendingAmt)
            {
                SysSession.WriteLog("Payment not enougth " + payment.TranID);
                return new OperationResult(false, "Payment not enougth " + payment.TranID);
            }

            foreach (var i in InvoicesAppls.ToList())
            {
                if ((invoice = _context.Invoices.Where(p => p.InvcKey == i.Item1).FirstOrDefault()) == null)
                {
                    SysSession.WriteLog("Invoice Not Found " + i.Item1);
                    return new OperationResult(false, "Invoice Not Found");
                }
                if (invoice.BalanceHC < i.Item2)
                {
                    SysSession.WriteLog("Overpay in invoice " + invoice.TranID);
                    return new OperationResult(false, "Overpay in invoice " + invoice.TranID);
                }
                pendings.Add(new PendingCustomerPaymentAppl()
                {
                    AppliedAtCreation = 0,
                    SeqNo = seqNo,
                    ApplyFromPmtKey = PaymentKey,
                    ApplyFromTranID = payment.TranID,
                    ApplyFromTranDate = payment.TranDate,
                    ApplyToInvcKey = i.Item1,
                    ApplyToTranID = invoice.TranID,
                    ApplyToTranDate = invoice.TranDate,
                    CurrExchRate = payment.CurrExchRate,
                    PmtAmt = i.Item2,
                    PmtAmtIC = Math.Round(i.Item2 * (decimal)payment.CurrExchRate, 2),
                    DiscTakenAmt = 0,
                    DiscTakenAmtIC = 0,
                    WriteOffAmt = 0,
                    WriteOffAmtIC = 0,
                    RealGainLossAmt = 0,
                    UpdateCounter = 0,
                    CreateUserID = SysSession.UserID,
                    CreateDate = DateTime.Now,
                    BatchKey = this.BatchKey,
                });
                seqNo++;
            }

            try
            {
                foreach (PendingCustomerPaymentAppl i in pendings)
                    _context.PendingCustomerPaymentAppls.AddObject(i);
                payment.UnappliedAmt -= pendings.Sum(p => p.PmtAmt);
                payment.UnappliedAmtHC -= pendings.Sum(p => p.PmtAmtIC);
                log.NextSeqNo = seqNo;
                _context.SaveChanges();
            }
            catch(Exception exc)
            {
                return new OperationResult(false, exc.Message);
            }
            return new OperationResult(true);
        }

        public static PaymentApplicationBatch CreatePaymentApplicationBatch(SageSession session, DateTime PostDate, string Cmnt = "")
        {
            ARBatch batch;
            int Key = Create(session, BatchTypes.PaymentInvoiceAppls, PostDate, Cmnt);
            EntityConnectionStringBuilder connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder()
            {
                Metadata = "res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = session.GetConnectionString()
            };
            Context context = new Context(connectionString.ToString());
            if (Key > 0 && (batch = context.ARBatches.Where(p => p.BatchKey == Key).FirstOrDefault()) != null)
            {
                return new PaymentApplicationBatch(session, Key);
            }
            return null;
        }

        protected override void InitializateRegistration()
        {
            base.InitializateRegistration();
            AROption options = _context.AROptions.Where(p => p.CompanyID == SysSession.CompanyID).FirstOrDefault();
            TrackTaxOnSales = options.TrackSTaxOnSales > 0 ? true : false;
            IntegratedWithCM = options.IntegrateWithCM > 0 ? true : false;
            UseMultiCurrency = options.UseMultCurr > 0 ? true : false;
        }

        protected override OperationResult ValidatePostingCurrencies()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("spARValGLCurr", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@_iBatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@_iHomeCurrency", SysSession.HomeCurrID);
                cmd.Parameters.AddWithValue("@_iCompanyID", SysSession.CompanyID);
                cmd.Parameters.Add(new SqlParameter("@_oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if(result == null)
                {
                    if (cmd.Parameters[3].Value != null && int.Parse(cmd.Parameters[3].Value.ToString()) != 0)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true);
                }
                cmd.Dispose();
            }
            return result;
        }

        protected override OperationResult CheckBalance()
        {
            SqlCommand cmd;
            int retVal = 0;
            OperationResult result = null;
            cmd = new SqlCommand("sparCheckBalance", SysSession.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@BatchKey", BatchKey);
                cmd.Parameters.AddWithValue("@SessionID", 1);
                cmd.Parameters.Add(new SqlParameter("@RetVal", retVal)
                {
                    DbType = System.Data.DbType.Int16,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                result = new OperationResult(false, exc.Message);
            }
            finally
            {
                if (result == null)
                {
                    if (cmd.Parameters[2].Value != null && int.Parse(cmd.Parameters[2].Value.ToString()) != 1)
                        result = new OperationResult(false, "Error");
                    else
                        result = new OperationResult(true); 
                }
                cmd.Dispose();
            }
            return result;
        }

        protected override void ShowRegistrationReports()
        {
            List<PaymentReportEntry> payments = new List<PaymentReportEntry>();
            List<PaymentApplReportEntry> paymentsAppls = new List<PaymentApplReportEntry>();
            Dictionary<int, decimal> invoices = new Dictionary<int, decimal>();
            CustomerPayment payment;
            foreach(var i in _context.PendingCustomerPaymentAppls.Where(p => p.BatchKey == BatchKey).ToList().GroupBy(p => p.ApplyFromPmtKey.Value))
            {
                payment = _context.CustomerPayments.Where(p => p.CustPmtKey == i.Key).FirstOrDefault();
                payments.Add(new PaymentReportEntry()
                {
                    BatchID = this.BatchID,
                    CustPmtKey = i.Key,
                    Payment = payment.TranID,
                    PaymentDate = payment.TranDate,
                    CurrID = payment.CurrID,
                    Customer = payment.Customer.CustID,
                    CustName = payment.Customer.CustName,
                    CustClass = payment.CustomersClass.CustClassID,
                });
                foreach(var j in i)
                {
                    if (!invoices.ContainsKey(j.ApplyToInvcKey.Value))
                        invoices.Add(j.ApplyToInvcKey.Value, _context.Invoices.Where(p => p.InvcKey == j.ApplyToInvcKey.Value).Select(p => p.BalanceHC).FirstOrDefault());
                    invoices[j.ApplyToInvcKey.Value] -= j.PmtAmtIC;
                    paymentsAppls.Add(new PaymentApplReportEntry()
                    {
                        CustPmtKey = i.Key,
                        Invoice = j.ApplyToTranID,
                        InvoiceDate = j.ApplyToTranDate,
                        PostAmt = j.PmtAmt,
                        PostAmtHC = j.PmtAmtIC,
                        WriteOffAmt = j.WriteOffAmt,
                        DiscountAmt = j.DiscTakenAmt,
                        InvoiceAmtHC = invoices[j.ApplyToInvcKey.Value],
                    });
                }
            }

            Reports.PaymentAppReport report = new Reports.PaymentAppReport();
            report.Database.Tables[0].SetDataSource(payments);
            report.Database.Tables[1].SetDataSource(paymentsAppls);
            CrystalReportEngine reporter = new CrystalReportEngine();
            reporter.SysSession = SysSession;
            reporter.Report = report;
            reporter.Show();
        }
    }
}
