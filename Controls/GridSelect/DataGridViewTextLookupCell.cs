using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    public class DataGridViewTextLookupCell : DataGridViewTextBoxCell
    {
        public IEnumerable<object> DataSource { get; set; }
        public SageSession SysSession { get; set; }
        public DataGridViewTextLookupCell() : base()
        {
            this.Style.Format = "d";
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            TextLookupEditingControl cec = DataGridView.EditingControl as TextLookupEditingControl;
            cec.SysSession = SysSession;
            cec.SetData(DataSource);
            if (this.Value == null)
                cec.Text = this.DefaultNewRowValue.ToString();
            else
            {
                try
                {
                    cec.Text = this.Value.ToString();
                }
                catch
                {
                    cec.Text = "";
                }
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(TextLookupEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return "";
            }
        }
    }
}
