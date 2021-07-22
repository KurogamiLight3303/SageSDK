using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Controls.GridSort
{
    [AttributeUsage(AttributeTargets.Property)]
    public class SortSelectField : Attribute
    {
        public SortSelectField(string Header)
        {

        }
    }
}
