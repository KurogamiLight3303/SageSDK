using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    public class DataGridViewStaticDropDownColumn : DataGridViewColumn
    {
        public DataGridViewStaticDropDownColumn() : base(new DataGridViewStaticDropDownCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate; set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewStaticDropDownCell)))
                    throw new InvalidCastException("Most be a DataGridViewStaticDropDownCell");
                base.CellTemplate = value;
            }
        }
    }
}
