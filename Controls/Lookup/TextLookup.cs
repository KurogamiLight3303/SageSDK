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
    [DefaultEvent("OnValueChange")]
    public partial class TextLookup : UserControl
    {
        protected LookupForm form;
        protected IEnumerable<object> data;
        protected int _key;
        protected bool _protected;
        protected bool _enable;

        public event LookupReturnEventHandler OnLookupReturn;
        public event EventHandler OnValueChange;
        public event EventHandler OnBeforeValueChange;
        public SageSession SysSession { get; set; }
        [Bindable(true)]
        public override string Text {
            get {
                return lkuText.Text;
            }
            set {
                lkuText.Text = value;
            }
        }
        [Bindable(true)]
        public int Key { get { return _key; } set {
                if(value > 0)
                {
                    lkuText.Tag = ValidateFromDataSource(value);
                    if (lkuText.Tag.ToString().Length == 0)
                        value = 0;
                    else
                        OnBeforeValueChange?.Invoke(this, new EventArgs());
                }
                else
                {
                    lkuText.Tag = string.Empty;
                    Text = string.Empty;
                }
                lkuText.Text = lkuText.Tag.ToString();
                if (value != _key)
                {
                    _key = value;
                    OnValueChanged(null);
                    OnValueChange?.Invoke(this, new EventArgs());
                }
                
            } }

        public string ErrorMessage { get; set; }

        public bool Protected {
            get
            {
                return _protected;
            }
            set
            {
                if (Enabled)
                    lkuText.ReadOnly = value;
                else
                    lkuText.ReadOnly = true;
                _protected = value;
            }
        }
        [DefaultValue(typeof(bool), "true")]
        public new bool Enabled
        {
            get
            {
                return _enable;
            }
            set
            {
                lkuBtn.Enable = value;
                _enable = value;
                Protected = !value;
            }
        }

        public bool TextOnlyLookup { get; set; }
        public string Mask { get { return lkuText.Mask; } set { lkuText.Mask = value; } }
        public TextLookup()
        {
            InitializeComponent();
        }
        public void SetData(IEnumerable<Object> data)
        {
            this.data = data;
            form = new LookupForm(data)
            {
                SysSession = this.SysSession
            };
            form.OnLookupReturn += Form_OnLookupReturn;
            lkuBtn.form = form;
            lkuBtn.SysSession = this.SysSession;
        }

        protected void Form_OnLookupReturn(object sender, LookupReturnEventArgs eventArgs)
        {
            PropertyInfo property;
            if(eventArgs.ReturnValue != null)
            {
                if((property = eventArgs.ReturnValue.GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault())!= null)
                    Text = property.GetValue(eventArgs.ReturnValue).ToString();
                try
                {
                    if (!TextOnlyLookup && (property = eventArgs.ReturnValue.GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupKeyReturn)).Count() != 0).FirstOrDefault()) != null)
                        Key = int.Parse(property.GetValue(eventArgs.ReturnValue).ToString());
                }
                catch
                {
                    Key = 0;
                }
            }

            if (this.OnLookupReturn != null)
                this.OnLookupReturn.Invoke(this, eventArgs);
        }

        protected int ValidateFromDataSource(string Text)
        {
            PropertyInfo property, returnProperty;

            if (data != null && data.Any())
            {
                property = data.FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault();
                returnProperty = data.FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupKeyReturn)).Count() != 0).FirstOrDefault();
                if (property != null && returnProperty != null)
                {
                    foreach (object i in data)
                        if (property.GetValue(i).ToString().ToLower() == Text.ToLower())
                        {
                            if (TextOnlyLookup)
                                return -1;
                            else
                                return (int)returnProperty.GetValue(i);
                        }
                }
            }
            return 0;
        }
        protected string ValidateFromDataSource(int keyValue)
        {
            PropertyInfo property, returnProperty;

            if (data != null && data.Any())
            {
                property = data.FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault();
                returnProperty = data.FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupKeyReturn)).Count() != 0).FirstOrDefault();
                if (property != null && returnProperty != null)
                {
                    foreach (object i in data)
                        if ((int)returnProperty.GetValue(i) == keyValue)
                            return property.GetValue(i).ToString();
                }
            }
            return string.Empty;
        }

        protected void Do_TextInput_Leave(object sender, EventArgs e)
         {
            int retValue;
            if(lkuText.Tag == null || lkuText.Tag.ToString() != lkuText.Text)
            {
                if (lkuText.Text.Length > 0)
                {
                    retValue = ValidateFromDataSource(lkuText.Text);
                    if(!TextOnlyLookup && retValue > 0)
                        Key = retValue;
                    else if (!TextOnlyLookup && retValue <= 0)
                    {
                        MessageBox.Show(string.Format((ErrorMessage == null || ErrorMessage.Length == 0)? "El valor {0} no es valido" : ErrorMessage, lkuText.Text), "Sage MAS 500", MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        lkuText.Text = "";
                        Key = 0;
                    }
                    lkuText.Tag = lkuText.Text;
                }
                else
                    Key = 0;
            }
        }

        protected virtual void OnValueChanged(EventArgs e)
        {

        }

        private void Validate_Text(object sender, CancelEventArgs e)
        {
            Do_TextInput_Leave(sender, e);
        }
        public object GetObjectByID(string textID)
        {
            if (form != null)
                return lkuBtn.GetObjectByID(textID);
            return null;
        }
    }
}
