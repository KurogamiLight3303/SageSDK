using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Net4Sage.Model;

namespace Net4Sage.Controls.DropDown
{
    /// <summary>
    /// Static DropDown Control
    /// </summary>
    [DefaultEvent("OnValueChange")]
    public partial class StaticDropDown : UserControl
    {
        private SageSession _session;
        /// <summary>
        /// The target table name 
        /// </summary>
        public string TableName { get; set; }
        /// <summary>
        /// The target field name 
        /// </summary>
        public string FieldName { get; set; }
        /// <summary>
        /// The Sage Session
        /// </summary>
        public SageSession SysSession { get => _session; set
            {
                if(_session != value)
                {
                    _session = value;
                    _session.OnSessionUpdate += OnInitialization;
                }
            }
        }
        /// <summary>
        /// Default Value
        /// </summary>
        public short DefaultValue { get; private set; }
        /// <summary>
        /// Event occurs when the selected value changes
        /// </summary>
        public event EventHandler OnValueChange;
        /// <summary>
        /// Selected Value
        /// </summary>
        [Bindable(true)]
        public object SelectedValue
        {
            get
            {
                return cbxComboBox.SelectedIndex > -1 ? cbxComboBox.SelectedValue : null;
            }
            set
            {
                try
                {
                    if (value != null)
                        cbxComboBox.SelectedItem = GetItemOfValue((short)value);
                    else
                        cbxComboBox.SelectedItem = GetItemOfValue(DefaultValue);
                }
                catch
                {
                    if(DefaultValue > -1)
                        cbxComboBox.SelectedItem = GetItemOfValue(DefaultValue);
                }
                OnValueChange?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Create new instance of the control
        /// </summary>
        public StaticDropDown() : base()
        {
            DefaultValue = -1;
            InitializeComponent();
        }
        /// <summary>
        /// Initialize the DropDown
        /// </summary>
        /// <param name="session">Sage Session to do the Initialization</param>
        public void InitializeDropDown(SageSession session)
        {
            SysSession = session;
            
            InitializeDropDown();
        }
        /// <summary>
        /// Initialize the DropDown
        /// </summary>
        public void InitializeDropDown()
        {
            OnInitialization(this, null);
        }
        private void OnInitialization(object sender, EventArgs e)
        {
            if (SysSession != null && SysSession.State == SessionStates.Connected)
            {
                MainBS.Clear();
                foreach (var i in SysSession.GetStaticValues(FieldName, TableName))
                {
                    if (i.IsDefault)
                        DefaultValue = i.Value;
                    if (!i.IsHidden)
                        MainBS.Add(i);
                }
            }
        }
        private StaticListEntry GetItemOfValue(short value)
        {
            foreach (StaticListEntry i in MainBS.List)
                if (i.Value == value)
                    return i;
            throw new Exception("Value not found");
        }
        /// <summary>
        /// Get values in the control
        /// </summary>
        /// <returns>Values</returns>
        public IEnumerable<StaticListEntry> GetValues()
        {
            return MainBS.List as IEnumerable<StaticListEntry>;
        }
    }
}
