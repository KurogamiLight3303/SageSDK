using Net4Sage.CIUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.ARUtils
{
    class InvoiceHandler : TransactionHandler
    {
        public InvoiceHandler(ref SageSession session) : base(ref session)
        {
            this.TranType = 401;
        }
    }
}
