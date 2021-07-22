using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.CIUtils
{
    public partial class CommonRegistrationForm : Form
    {
        public bool DoRegister { get { return chkRegister.Checked; } }
        public bool DoPost { get { return chkPost.Checked; } }
        public bool DoConfirmPost { get { return chkPostCheck.Checked; } }

        public CommonRegistrationForm()
        {
            InitializeComponent();
        }

        public CommonRegistrationForm(SageSession sysSession) : this()
        {
            SysSession.InitializeSession(sysSession);
        }

        private void On_CheckedChange(object sender, EventArgs e)
        {
            if (chkPost.Checked)
            {
                chkPostCheck.Checked = true;
                chkPostCheck.Enabled = true;
            }
            else
            {
                chkPostCheck.Checked = false;
                chkPostCheck.Enabled = false;
            }
        }

        private void On_Register_CheckedChange(object sender, EventArgs e)
        {
            if (chkRegister.Checked)
            {
                if (cbxOutput.SelectedIndex == -1)
                    cbxOutput.SelectedIndex = 0;
                chkPost.Checked = true;
                On_CheckedChange(sender, e);
            }
        }

        public void DisableAll()
        {
            chkPostCheck.Enabled = false;
            chkPost.Enabled = false;
            chkRegister.Enabled = false;
        }
    }
}
