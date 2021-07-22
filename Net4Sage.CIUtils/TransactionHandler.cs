using Net4Sage;
using Net4Sage.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.CIUtils
{
    public abstract class TransactionHandler
    {
        public static string FillLeftCeros(string No, int lenght)
        {
            while (No.Length < lenght)
                No = '0' + No;
            return No;
        }

        public static string FillLeftCeros(int No, int lenght)
        {
            return FillLeftCeros(No.ToString(), lenght);
        }

        protected SageSession SysSession;
        protected Context context;
        public int TranType { get; protected set; }
        public TransactionHandler(ref SageSession session)
        {
            SysSession = session;
            LoadContext();
        }

        protected virtual void LoadContext()
        {
            System.Data.EntityClient.EntityConnectionStringBuilder connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder()
            {
                Metadata = "res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = SysSession.GetConnectionString()
            };
            context = new Context(connectionString.ToString());
        }

        protected string GetNextTransactionNumber()
        {
            string answer = string.Empty;
            
            switch (TranType)
            {
                case (int)TranTypes.MF_WorkOrder:
                    MFParameters parameters = context.MFParameters.Where(p => p.CompanyID == SysSession.CompanyID).FirstOrDefault();
                    if(parameters != null)
                    {
                        answer = ((int)parameters.ParmNextWorkOrdNum).ToString();
                        parameters.ParmNextWorkOrdNum += 1;
                        context.SaveChanges();
                    }
                    break;
                default:
                    TranTypeCompany tranTypeCompany = context.TranTypeCompanies.Where(p => p.CompanyID == SysSession.CompanyID && p.TranType == (int)TranType).FirstOrDefault();
                    if (tranTypeCompany != null)
                    {
                        answer = tranTypeCompany.NextTranNo.ToString();
                        tranTypeCompany.NextTranNo += 1;
                        context.SaveChanges();
                    }
                    break;
            }
            return FillLeftCeros(answer, 10);
        }

        public static int? GetNextSurrogateKey(string tableName, ref SageSession session)
        {
            SqlCommand cmd;
            int NewKey = 0;
            int? result = null;
            cmd = new SqlCommand("spGetNextBatch", session.GetConnection());
            try
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iTableName", tableName);
                cmd.Parameters.Add(new SqlParameter("@oNewKey", NewKey)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,
                });
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                session.WriteLog(exc.Message);
            }
            finally
            {
                if (cmd.Parameters[1].Value != null)
                    result = int.Parse(cmd.Parameters[8].Value.ToString());
                cmd.Dispose();
            }
            return result;
        }
    }
}
