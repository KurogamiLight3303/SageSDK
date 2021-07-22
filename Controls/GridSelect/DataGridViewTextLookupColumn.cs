using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    public class DataGridViewTextLookupColumn : DataGridViewColumn
    {
        public DataGridViewTextLookupColumn() : base(new DataGridViewTextLookupCell())
        {

        }

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate; set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewTextLookupCell)))
                    throw new InvalidCastException("Most be a DataGridViewTextLookupCell");
                base.CellTemplate = value;
            }
        }
    }
}
