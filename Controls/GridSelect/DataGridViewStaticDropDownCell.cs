using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    public class DataGridViewStaticDropDownCell : DataGridViewComboBoxCell
    {
        public string TableName
        {
            get;set;
        }
        public string FieldName
        {
            get;set;
        }
        public SageSession SysSession
        {
            get;
            set;
        }
        public DataGridViewStaticDropDownCell() : base()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            StaticDropDownEditingControl cec = DataGridView.EditingControl as StaticDropDownEditingControl;
            cec.TableName = TableName;
            cec.FieldName = FieldName;
            cec.InitializeDropDown(SysSession);
            if (this.Value == null)
                cec.SelectedValue = this.DefaultNewRowValue;
            else
            {
                cec.SelectedValue = null;
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(StaticDropDownEditingControl);
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
                try
                {
                    StaticDropDownEditingControl cec = DataGridView.EditingControl as StaticDropDownEditingControl;
                    return cec.DefaultValue;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
