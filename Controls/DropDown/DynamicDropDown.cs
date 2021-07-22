using Net4Sage.Controls.Lookup;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Net4Sage.Controls.DropDown
{
    /// <summary>
    /// Dynamic Dropdown control
    /// </summary>
    [DefaultEvent("OnValueChange")]
    public partial class DynamicDropDown : UserControl
    {
        /// <summary>
        /// The Sage Session
        /// </summary>
        public SageSession SysSession { get; set; }
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
                        cbxComboBox.SelectedItem = null;
                }
                catch (Exception exc)
                {
                    SysSession?.WriteLog(exc);
                    cbxComboBox.SelectedItem = null;
                }
                OnValueChange?.Invoke(this, new EventArgs());
            }
        }
        /// <summary>
        /// Create new instance of the control
        /// </summary>
        public DynamicDropDown() : base()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Initialize the DropDown
        /// </summary>
        /// <param name="data">Data to</param>
        public void InitializeDropDown(IEnumerable<KeyValuePair<int, string>> data)
        {
            MainBS.Clear();
            foreach (KeyValuePair<int, string> i in data)
                MainBS.Add(i);

            cbxComboBox.SelectedValueChanged += delegate
            {
                OnValueChange?.Invoke(this, new EventArgs());
            };
        }
        /// <summary>
        /// Initialize DropDown with a collection of objects
        /// </summary>
        /// <param name="data">Collenction</param>
        public void InitializeDropDown(IEnumerable<object> data)
        {
            InitializeDropDown(data);
        }
        /// <summary>
        /// Initialize DropDown with a collection of objects
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="data">Collenction</param>
        public void InitializeDropDown<T>(IEnumerable<T> data)
        {
            PropertyInfo keyProperty, textProperty;
            T model;
            List<KeyValuePair<int, string>> values = new List<KeyValuePair<int, string>>();
            if ((model = data.FirstOrDefault()) != null)
            {
                keyProperty = model.GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupKeyReturn)).Count() != 0).FirstOrDefault();
                textProperty = model.GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault();
                foreach (T i in data)
                {
                    try
                    {
                        values.Add(new KeyValuePair<int, string>(int.Parse(keyProperty.GetValue(i).ToString()), textProperty.GetValue(i).ToString()));
                    }
                    catch (Exception exc)
                    {
                        SysSession?.WriteLog(exc);
                    }
                }
            }
            InitializeDropDown(values);
        }
        private KeyValuePair<int, string> GetItemOfValue(short value)
        {
            foreach (KeyValuePair<int, string> i in MainBS.List)
                if (i.Key == value)
                    return i;
            throw new Exception();
        }
        /// <summary>
        /// Get values in the control
        /// </summary>
        /// <returns>Values</returns>
        public IEnumerable<KeyValuePair<int, string>> GetValues()
        {
            return MainBS.List as IEnumerable<KeyValuePair<int, string>>;
        }
    }
}
