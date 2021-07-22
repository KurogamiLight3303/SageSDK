using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.ARUtils
{
    internal class PaymentApplReportEntry
    {
        public int CustPmtKey { get; set; }
        public string Invoice { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal InvoiceAmt { get; set; }
        public decimal InvoiceAmtHC { get; set; }
        public decimal DiscountAmt { get; set; }
        public decimal WriteOffAmt { get; set; }
        public decimal PostAmt { get; set; }
        public decimal PostAmtHC { get; set; }
    }
}
