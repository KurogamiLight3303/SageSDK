using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.GLUtils
{
    public class JournalAnnotationReference
    {
        private decimal _amt;
        public int No { get; set; }
        public string AccountNumber { get; set; }
        public string CurrID { get; set; }
        public decimal ExchRate { get; set; }
        public decimal PostAmt { get => Math.Round(_amt, 3); set => _amt = value; }
        public decimal PostAmtHC { get => Math.Round(PostAmt * ExchRate, 3); }
        public string Comment { get; set; }
    }
}
