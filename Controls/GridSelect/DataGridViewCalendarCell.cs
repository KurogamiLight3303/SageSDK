using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    public class DataGridViewCalendarCell : DataGridViewTextBoxCell
    {
        public DataGridViewCalendarCell() : base()
        {
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            CalendarEditingControl cec = DataGridView.EditingControl as CalendarEditingControl;
            if (this.Value == null)
                cec.Value = (DateTime)this.DefaultNewRowValue;
            else
            {
                try
                {
                    if (this.Value.GetType() == typeof(DateTime))
                        cec.Value = (DateTime)this.Value;
                    else
                        cec.Value = DateTime.Parse(this.Value.ToString());
                }
                catch
                {
                    cec.Value = (DateTime)this.DefaultNewRowValue;
                }
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(CalendarEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(DateTime);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return DateTime.Now;
            }
        }
    }
}
