using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage.Controls.Lookup;

namespace Net4Sage.DataAccessModel
{
    #region "IM"
    public partial class Item
    {
        [LookupShowColumn]
        [LookupColumnHeader("Descripción")]
        [LookupColumnHeaderWidth(200)]
        public string Description { get { if (this.Descriptions.Count > 0) return this.Descriptions.FirstOrDefault().ToString(); return "asdasd"; } }

        public override string ToString()
        {
            return this.ItemID;
        }
    }
    public partial class ItemDescription
    {
        public override string ToString()
        {
            return this.ShortDesc;
        }
    }
    #endregion

    #region "SO"
    public partial class Customer
    {
        public override string ToString()
        {
            return this.CustID;
        }
    }

    public partial class SalesOrder
    {
        public override string ToString()
        {
            return this.TranNo;
        }
    }
    #endregion
}
