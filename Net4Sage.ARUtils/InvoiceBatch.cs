using Net4Sage.CIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.ARUtils
{
    public class InvoiceBatch : Batch
    {
        public InvoiceBatch(SageSession session, int BatchKey) : base(session, BatchKey)
        {
        }
    }
}
