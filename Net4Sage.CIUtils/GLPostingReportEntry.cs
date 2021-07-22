using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.CIUtils
{
    public class GLPostingReportEntry
    {
        public string BatchID { get; set; }
        public string BatchDescription { get; set; }
        public string BatchType { get; set; }
        public DateTime PostDate { get; set; }
        public string JournalID { get; set; }
        public string Currency { get; set; }
        public string JournalDescription { get; set; }
        public DateTime Date { get; set; }
        public string Account { get; set; }
        public string Description { get; set; }
        public decimal PostAmt { get; set; }
        public decimal PostAmtHC { get; set; }
    }
}
