using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    public class DataGridViewCalendarColumn : DataGridViewColumn
    {
        public DataGridViewCalendarColumn() : base(new DataGridViewCalendarCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get => base.CellTemplate; set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewCalendarCell)))
                    throw new InvalidCastException("Most be a DataGridViewCalendarCell");
                base.CellTemplate = value;
            }
        }
    }
}
