using Net4Sage.CIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage.DataAccessModel;
using System.Globalization;
using System.Data.SqlClient;

namespace Net4Sage.IMUtils
{
    public class InventoryTransactionHandler : TransactionHandler
    {
        public InventoryTransactionHandler(ref SageSession session) : base(ref session)
        {
        }

        public void MoveInventaryInWarehouse(int itemKey, double Qty, int whseBinFrom, int whseBinTo, int? lotKey = null, int? serialKey = null)
        {
            int? retVal = 0;
            SqlCommand cmd;
            WhseBin from = context.WhseBins.Where(p => p.WhseBinKey == whseBinFrom).FirstOrDefault(), to = context.WhseBins.Where(p => p.WhseBinKey == whseBinTo).FirstOrDefault();
            if (from.WhseKey != to.WhseKey)
                throw new Exception("The transaction most be in the same warehouse");
            Item item = context.Items.Where(p => p.ItemKey == itemKey).FirstOrDefault();
            UnitMeasure measure = item.StockUM;
            decimal avaliableQty = 0;
            InvtLotBin fromLotbin = context.InvtLotBins.Where(p => p.WhseBinKey == from.WhseBinKey && p.InvtLotKey == lotKey.Value).FirstOrDefault();
            InvtLotBin toLotBin = context.InvtLotBins.Where(p => p.WhseBinKey == to.WhseBinKey && p.InvtLotKey == lotKey.Value).FirstOrDefault();

            if(toLotBin == null)
            {
                context.GetNextSurrogateKey("timInvtLotBin", ref retVal);
                toLotBin = new InvtLotBin() {
                    InvtLotKey = lotKey.Value,
                    WhseBinKey = to.WhseBinKey,
                    InvtLotBinKey = retVal.Value
                };

                context.InvtLotBins.AddObject(toLotBin);
                context.SaveChanges();
            }

            if(fromLotbin != null)
                avaliableQty = fromLotbin.OrigQty - fromLotbin.QtyUsed;
            
            SysSession.GetConnection();
            cmd = new SqlCommand("select * into #timStockWrk from timStockWrk where null is not null", SysSession.GetConnection());
            cmd.ExecuteNonQuery();
            try
            {
                try
                {
                    cmd = new SqlCommand("insert into #timStockWrk values (null, '" + SysSession.CompanyID + "', null," + itemKey + ", " + (lotKey.HasValue ? lotKey.Value.ToString() : "null") + ", " + (lotKey.HasValue ? "'" + fromLotbin.InvtLot.LotNo + "'" : "null") + ", " + avaliableQty.ToString("0.0", CultureInfo.InvariantCulture) + ", " + Qty.ToString("0.0", CultureInfo.InvariantCulture) + ", null, null, " + to.WhseBinKey + ", " + from.WhseBinKey + ", " + avaliableQty.ToString("0.0", CultureInfo.InvariantCulture) + ", " + Qty.ToString("0.0", CultureInfo.InvariantCulture) + "," + measure.UnitMeasKey + ", " + measure.UnitMeasKey + ")", SysSession.GetConnection());
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    cmd = new SqlCommand("insert into #timStockWrk values ('"+ toLotBin.WhseBin.WhseBinID + "', '" + SysSession.CompanyID + "', null," + itemKey + ", " + (lotKey.HasValue ? lotKey.Value.ToString() : "null") + ", " + (lotKey.HasValue ? "'" + fromLotbin.InvtLot.LotNo + "'" : "null") + ", " + avaliableQty.ToString("0.0", CultureInfo.InvariantCulture) + ", " + Qty.ToString("0.0", CultureInfo.InvariantCulture) + ", null, null, " + to.WhseBinKey + ", " + from.WhseBinKey + ")", SysSession.GetConnection());
                    cmd.ExecuteNonQuery();
                }
                
                cmd = new SqlCommand("spimSaveBinTransferTrnxs", SysSession.GetConnection());
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@iCompanyID", SysSession.CompanyID);
                cmd.Parameters.AddWithValue("@iBusinessDate", SysSession.BusinessDate);
                cmd.Parameters.Add(new SqlParameter("@oRetVal", retVal)
                {
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Output,  
                });
                cmd.ExecuteNonQuery();
            }
            catch(Exception exc)
            {
                cmd = new SqlCommand("drop table #timStockWrk", SysSession.GetConnection());
                cmd.ExecuteNonQuery();
                throw new Exception("Error: " + exc.Message);
            }
            cmd = new SqlCommand("drop table #timStockWrk", SysSession.GetConnection());
            cmd.ExecuteNonQuery();
        }
    }
}
