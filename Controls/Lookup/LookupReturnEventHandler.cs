using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Controls.Lookup
{
    public class LookupReturnEventArgs : EventArgs
    {
        public Object ReturnValue { get; set; }
    }
    public delegate void LookupReturnEventHandler(object sender, LookupReturnEventArgs eventArgs);
}