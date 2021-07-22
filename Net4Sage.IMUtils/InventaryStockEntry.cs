using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.IMUtils
{
    public class InventaryStockEntry
    {
        public int ItemKey { get; set; }
        public string ItemID { get; set; }
        public int? SerialKey { get; set; }
        public string Serial { get; set; }
        public int? LotKey { get; set; }
        public string Lot { get; set; }
        public string Bin { get; set; }
        public int WhseBinKey { get; set; }
        public string Warehouse { get; set; }
        public int WhseKey { get; set; }
        public decimal AvalibleQty { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
