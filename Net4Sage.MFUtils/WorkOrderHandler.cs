using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage;
using Net4Sage.DataAccessModel;
using Net4Sage.CIUtils;

namespace Net4Sage.MFUtils
{
    public class WorkOrderHandler : TransactionHandler
    {
        
        public WorkOrderHandler(ref SageSession session) : base(ref session)
        {
            TranType = (int)TranTypes.MF_WorkOrder;
        }

        public WorkOrder CreateWorkOrder(int RoutingKey, decimal Qty, DateTime startDate, DateTime requireDate, DateTime releaseDate, DateTime entryDate)
        {
            Route route = context.Routes.Where(p => p.RoutingKey == RoutingKey).FirstOrDefault();
            if (!route.WhseKey.HasValue) throw new Exception("Warehouse not found");

            string TranNo = GetNextTransactionNumber();
            int? woKey = 0, temp = 0;
            string backFlush;

            backFlush = route.Details.Where(p => p.ProgressStep == "Y").FirstOrDefault().BackFlush;

            context.GetNextSurrogateKey("tmfWorkOrdHead_HAI", ref woKey);

            WorkOrder order = new WorkOrder()
            {
                WorkOrderKey = woKey.Value,
                RoutingKey = route.RoutingKey,
                MFGCommitDate = startDate,
                RequiredDate = requireDate,
                ReleaseDate = releaseDate,
                EntryDate = entryDate,
                CompanyID = SysSession.CompanyID,
                CreateType = 0,
                WorkOrderType = 0,
                WhseKey = route.WhseKey.Value,
                ItemClassKey = route.ItemClassKey,
                QTYCycle = route.QTYCycle,
                Quantity = Qty,
                WorkOrderNo = TranNo,
                CustPONo = "",
                CreateDate = DateTime.Today,
                CreateUserID = SysSession.UserID
            };

            WorkOrderProd prod = new WorkOrderProd()
            {
                CompanyID = SysSession.CompanyID,
                ItemKey = route.ItemKey,
                QTYCycle = route.QTYCycle,
                QTYTotal = Qty,
                WorkOrderKey = woKey.Value,
            };

            context.WorkOrders.AddObject(order);
            context.WorkOrderProds.AddObject(prod);
            context.SaveChanges();

            context.CreateWorkOrderSteps(SysSession.CompanyID, woKey, route.RoutingKey, 0, 0, backFlush, 1, 0, ref temp, 1);

            return context.WorkOrders.Where(p => p.WorkOrderKey == woKey).FirstOrDefault();
        }
    }
}
