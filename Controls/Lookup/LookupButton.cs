using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace Net4Sage.Controls.Lookup
{
    /// <summary>
    /// Lookup Button Control
    /// </summary>
    [DefaultEvent("OnLookupReturn")]
    public partial class LookupButton : UserControl
    {
        internal LookupForm form;
        private bool _enable;
        /// <summary>
        /// Session used by the control
        /// </summary>
        public SageSession SysSession { get; set; }
        /// <summary>
        /// Get the amount of data load for the form
        /// </summary>
        public int DataCount { get { return form != null ? form.DataCount : 0; } }
        /// <summary>
        /// Set/Get the Enable state of the control
        /// </summary>
        public bool Enable
        {
            get
            {
                return _enable;
            }
            set
            {
                if(value)
                    btnLookup.Image = global::Net4Sage.Properties.Resources.srch_16;
                else
                    btnLookup.Image = global::Net4Sage.Properties.Resources.Disable_Lookup;
                _enable = value;
            }
        }
        /// <summary>
        /// Event triger when the lookup form returns
        /// </summary>
        public event LookupReturnEventHandler OnLookupReturn;
        /// <summary>
        /// Event triger when the data source changes
        /// </summary>
        public event EventHandler OnDataSourceChange;
        /// <summary>
        /// Create instance of the control
        /// </summary>
        public LookupButton()
        {
            InitializeComponent();
            Enable = true;
        }
        /// <summary>
        /// Set Datasource of the Lookup
        /// </summary>
        /// <param name="data">Collection of Objects</param>
        public void SetData(IEnumerable<Object> data)
        {
            form = new LookupForm(data);
            form.OnLookupReturn += Form_OnLookupReturn;
            OnDataSourceChange?.Invoke(this, null);
        }
        /// <summary>
        /// Get object of the DataSource
        /// </summary>
        /// <param name="textID">TextID</param>
        /// <returns>Object Found</returns>
        public object GetObjectByID(string textID)
        {
            if (form != null)
                return form.GetObjectByID(textID);
            return null;
        }
        private void Form_OnLookupReturn(object sender, LookupReturnEventArgs eventArgs)
        {
            if (OnLookupReturn != null)
                OnLookupReturn.Invoke(this, eventArgs);
        }
        private void Open_Form(object sender, EventArgs e)
        {
            if (Enable)
                if (form != null && form.HaveData)
                {
                    form.SysSession = SysSession;
                    form.ShowDialog();
                }
                else
                    MessageBox.Show("No hay datos para mostrar", "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal bool MoveTo(int index)
        {
            if (form != null)
                return form.MoveTo(index);
            return false;
        }
        internal int IndexOf(object obj)
        {
            return form != null ? form.IndexOf(obj) : -1;
        }
    }
}
