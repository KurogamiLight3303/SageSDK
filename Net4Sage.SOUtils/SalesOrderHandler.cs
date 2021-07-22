using Net4Sage.CIUtils;
using Net4Sage.DataAccessModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.SOUtils
{
    public class SalesOrderHandler : TransactionHandler
    {
        public SalesOrderHandler(ref SageSession session) : base(ref session)
        {
        }

        public SalesOrder CreateSalesOrder(int CustKey,int WhseKey, DateTime date, IEnumerable<SOLineImportEntry> lines, bool useStdPrice = false)
        {
            int?  soKey = 0, sPID = 0;
            short? retVal = 0;

            SalesOrderImport so = new SalesOrderImport()
            {
                CompanyID = SysSession.CompanyID,
                CrHold = 0,
                CurrExchRate = 1,
                CurrID = SysSession.HomeCurrID,
                CustKey = CustKey,
                ErrorStatus = 0,
                TranType = 801,
                TranDate = date,
                ReqDelvDate = date,
                WhseKey = WhseKey,
            };

            try
            {
                context.SalesOrderImports.AddObject(so);
                context.SaveChanges();
            }
            catch (Exception e)
            {

            }
            foreach (var item in (from l in lines
                                  join i in context.Items on l.ItemKey equals i.ItemKey
                                  select new SOLineImport()
                                  {
                                      ItemKey = l.ItemKey,
                                      QtyOrd = l.Qty,
                                      SalesOrderImportKey = so.SalesOrderImportKey,
                                      Description = i.Description,
                                      ItemID = i.ItemID,
                                      UnitMeasKey = i.SalesUnitMeasKey,
                                      UnitPrice = useStdPrice ? i.StdPrice : l.UnitPrice
                                  }))
            {
                try
                {
                    context.SOLineImports.AddObject(item);
                    context.SaveChanges();
                }catch(Exception exc)
                {
                    return null;
                }
            }

            try
            {
                
                context.SOImportSalesOrder(SysSession.CompanyID, SysSession.UserID, so.SalesOrderImportKey, ref retVal, ref sPID, ref soKey);
                if(retVal == 1 || retVal == 2)
                {
                    return context.SalesOrders.Where(p => p.SOKey == soKey).FirstOrDefault();
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }
    }
}
