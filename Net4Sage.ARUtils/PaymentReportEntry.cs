using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.ARUtils
{
    internal class PaymentReportEntry
    {
        public int CustPmtKey { get; set; }
        public string Customer { get; set; }
        public string CustName { get; set; }
        public string CustClass { get; set; }
        public string CurrID { get; set; }
        public string Payment { get; set; }
        public DateTime PaymentDate { get; set; }
        public string BatchID { get; set; }
    }
}
