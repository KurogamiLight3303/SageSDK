using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage.DataAccessModel;

namespace Net4Sage.IMUtils
{
    public class InventaryHandler
    {
        private readonly SageSession SysSession;
        private Context context;
        public InventaryHandler(ref SageSession session)
        {
            SysSession = session;
            System.Data.EntityClient.EntityConnectionStringBuilder connectionString = new System.Data.EntityClient.EntityConnectionStringBuilder()
            {
                Metadata = "res://*/DataModel.csdl|res://*/DataModel.ssdl|res://*/DataModel.msl",
                Provider = "System.Data.SqlClient",
                ProviderConnectionString = SysSession.GetConnectionString()
            };
            context = new Context(connectionString.ToString());
        }

        public decimal GetAvaliableQty(int itemKey, int? warehouseKey = null)
        {
            decimal answer = 0;
            foreach(Warehouse whse in context.Inventories.Where(p => p.ItemKey == itemKey && (!warehouseKey.HasValue || p.WhseKey == warehouseKey.Value)).Select(p => p.Warehouse).ToList())
            {
                if (whse.UseBins == 1)
                    foreach (WhseBin bin in whse.WhseBins.Where(p => p.ExclFromAvail == 0).ToList())
                        answer += GetQtyInWarehouseBin(itemKey, bin.WhseBinKey);
            }
            return answer;
        }

        public decimal GetItemQty(int itemKey, int? warehouseKey = null)
        {
            decimal answer = 0;
            try
            {
                foreach (Inventory invt in context.Inventories.Where(p => p.ItemKey == itemKey && (!warehouseKey.HasValue || p.WhseKey == warehouseKey.Value)).ToList())
                    answer += invt.InvtTrans.ToList().Sum(p => p.TranQty);
            }
            catch (Exception) { }
            return answer;
        }

        public decimal GetItemAmt(int itemKey, int? warehouseKey = null)
        {
            decimal answer = 0;
            try
            {
                foreach (Inventory invt in context.Inventories.Where(p => p.ItemKey == itemKey && (!warehouseKey.HasValue || p.WhseKey == warehouseKey.Value)).ToList())
                    answer += invt.InvtTrans.ToList().Sum(p => p.TranAmt);
            }
            catch (Exception) { }
            return answer;
        }

        public decimal GetQtyInWarehouseBin(int itemKey, int whseBinKey)
        {
            int warehouseKey = context.WhseBins.Where(p => p.WhseBinKey == whseBinKey).FirstOrDefault().WhseKey;
            decimal answer = 0;
            foreach(InvtTran tran in context.InvtTrans.Where(p => p.ItemKey == itemKey && p.WhseKey == warehouseKey && p.InvtTranDists.Where(s => s.WhseBinKey == whseBinKey).Count() > 0).ToList())
            {
                answer += tran.InvtTranDists.Where(p => p.WhseBinKey == whseBinKey).Sum(p => p.DistQty) * (tran.TranQty > 0 ? 1 : -1) ;
            }
            return answer;
        }

        public decimal GetQtyInWarehouseBin(int itemKey, string whseBin, int? warehouseKey = null)
        {
            decimal answer = 0;
            foreach (WhseBin bin in context.Inventories.Where(p => p.ItemKey == itemKey && (!warehouseKey.HasValue || p.WhseKey == warehouseKey.Value)).SelectMany(p => p.Warehouse.WhseBins).Where(p => p.WhseBinID == whseBin).ToList())
                answer += GetQtyInWarehouseBin(itemKey, bin.WhseBinKey);
            return answer;
        }

        public IEnumerable<string> GetWarehouseBins(int? warehouseKey = null)
        {
            List<string> answer = new List<string>();
            foreach (Warehouse whse in context.Inventories.Where(p => (!warehouseKey.HasValue || p.WhseKey == warehouseKey.Value && p.Warehouse.UseBins == 1)).Select(p => p.Warehouse).ToList())
                foreach (WhseBin bin in whse.WhseBins)
                    if (!answer.Contains(bin.WhseBinID))
                        answer.Add(bin.WhseBinID);

            return answer;
        }

        public IEnumerable<InventaryStockEntry> GetInventaryStatus(int itemKey,bool onlyAvaliable = false, int? warehousekey = null)
        {
            Item item = context.Items.Where(p => p.ItemKey == itemKey).FirstOrDefault();
            List<InventaryStockEntry> answer = new List<InventaryStockEntry>();
            foreach (Warehouse whse in context.Inventories.Where(p => p.ItemKey == itemKey && (!warehousekey.HasValue || p.WhseKey == warehousekey.Value)).Select(p => p.Warehouse).ToList())
            {
                foreach (WhseBin bin in whse.WhseBins.Where(p => onlyAvaliable == false || p.ExclFromAvail == 0))
                {
                    switch (item.TrackMeth)
                    {
                        case ItemTrackMeth.Lot:
                            foreach(InvtLotBin ilb in context.InvtLots.Where(p => p.ItemKey == itemKey && p.WhseKey == whse.WhseKey).SelectMany(p => p.InvtLotBins).Where(p => p.WhseBinKey == bin.WhseBinKey).ToList())
                                if(!onlyAvaliable || ilb.OrigQty - ilb.QtyUsed - ilb.PendQtyDecrease > 0)
                                    answer.Add(new InventaryStockEntry()
                                    {
                                        ItemKey = itemKey,
                                        ItemID = item.ItemID,
                                        WhseKey = whse.WhseKey,
                                        Warehouse = whse.WhseID,
                                        WhseBinKey = bin.WhseBinKey,
                                        Bin = bin.WhseBinID,
                                        Lot = ilb.InvtLot.LotNo,
                                        LotKey = ilb.InvtLotKey,
                                        Serial = "",
                                        SerialKey = null,
                                        AvalibleQty = ilb.OrigQty - ilb.QtyUsed - ilb.PendQtyDecrease,
                                        ExpirationDate = ilb.InvtLot.ExpirationDate,
                                    });
                            break;
                        case ItemTrackMeth.None:
                            answer.Add(new InventaryStockEntry()
                            {
                                ItemKey = itemKey,
                                ItemID = item.ItemID,
                                WhseKey = whse.WhseKey,
                                Warehouse = whse.WhseID,
                                WhseBinKey = bin.WhseBinKey,
                                Bin = bin.WhseBinID,
                                Lot = "",
                                LotKey = null,
                                Serial = "",
                                SerialKey = null,
                                AvalibleQty = GetQtyInWarehouseBin(itemKey, bin.WhseBinKey)
                            });
                            break;
                    }
                }
            }
            return answer;
        }

    }
}
