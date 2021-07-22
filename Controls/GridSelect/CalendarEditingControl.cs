using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    public class CalendarEditingControl : DateTimePicker, IDataGridViewEditingControl
    {
        int rowIndex;
        private bool valueChanged = false;
        DataGridView dataGridView;
        public CalendarEditingControl()
        {
            this.Format = DateTimePickerFormat.Short;
        }
        public DataGridView EditingControlDataGridView { get => dataGridView; set => dataGridView = value; }
        public object EditingControlFormattedValue
        {
            get
            {
                return this.Value.ToShortDateString();
            }
            set {
                if(value is string)
                {
                    try
                    {
                        this.Value = DateTime.Parse(value as string).Date;
                    }
                    catch
                    {
                        this.Value = DateTime.Now.Date;
                    }
                }
            }
        }
        public int EditingControlRowIndex { get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }
        public bool EditingControlValueChanged { get { return valueChanged; } set { valueChanged = value; } }

        public Cursor EditingPanelCursor { get { return base.Cursor; } }

        public bool RepositionEditingControlOnValueChange { get { return false; } }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.CalendarForeColor = dataGridViewCellStyle.ForeColor;
            this.CalendarMonthBackground = dataGridViewCellStyle.BackColor;
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch(keyData & Keys.KeyCode)
            {
                case Keys.Left:
                case Keys.Up:
                case Keys.Down:
                case Keys.Right:
                case Keys.Home:
                case Keys.End:
                case Keys.PageDown:
                case Keys.PageUp:
                    return true;
                default:
                    return dataGridViewWantsInputKey;
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnValueChanged(eventargs);
        }
    }
}
