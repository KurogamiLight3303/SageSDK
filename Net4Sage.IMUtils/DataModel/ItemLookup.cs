using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Net4Sage.Controls.Lookup;

namespace Net4Sage.IMUtils.DataModel
{
    [LookupFormTitle("Buscar Artículo")]
    public class ItemLookup
    {
        [LookupHideColumn]
        public int ItemKey { get; set; }
        [LookupTextReturn]
        [LookupColumnHeader("ID de Artículo")]
        [LookupColumnFilter("ID de Artículo")]
        public string ItemID { get; set; }
        public string Description { get; set; }
        public short ItemType { get; set; }
    }
}
