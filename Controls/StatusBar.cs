using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Net4Sage.Controls.Navigators;

namespace Net4Sage.Controls
{
    /// <summary>
    /// Status of the form
    /// </summary>
    public enum FormBindingStatus
    {
        /// <summary>
        /// None
        /// </summary>
        None = 0,
        /// <summary>
        /// Waiting for the User
        /// </summary>
        Waiting = 1,
        /// <summary>
        /// Adding New Values
        /// </summary>
        Adding = 2,
        /// <summary>
        /// Editing Values
        /// </summary>
        Editing = 3
    }

    public partial class StatusBar : UserControl
    {
        private SageSession _session;
        /// <summary>
        /// Sage Session to Use with the Control
        /// </summary>
        public SageSession SysSession { get => _session; set
            {
                if (_session != value)
                {
                    _session = value;
                    _session.OnSessionUpdate += UpdateSessionData;
                }
            }
        }
        /// <summary>
        /// Navigator to Use with the Control
        /// </summary>
        public Navigator Navigator { get => nvcController.Navigator; set
            {
                if(nvcController.Navigator != value)
                {
                    nvcController.Navigator = value;
                    Navigator.Initializer();
                    nvcController.Visible = true;
                    nvcController.UpdateNavigator();
                }
            }
        }
        /// <summary>
        /// Create instance of Status Bar
        /// </summary>
        public StatusBar()
        {
            InitializeComponent();
            this.Dock = DockStyle.Bottom;
        }
        /// <summary>
        /// Show/Hide the progress bar
        /// </summary>
        /// <param name="status">Show or Hide</param>
        public void ToggleProgressBar(bool status)
        {
            try
            {
                toolStripProgressBar1.Visible = status;
            }
            catch(Exception exc)
            {
                SysSession.WriteLog(exc);
            }
        }
        /// <summary>
        /// Set the progress of the progress bar
        /// </summary>
        /// <param name="number">Porcente between 0 and 100</param>
        public void SetProgressBar(int number)
        {
            try
            {
                if (!toolStripProgressBar1.Visible)
                    ToggleProgressBar(true);
                toolStripProgressBar1.Value = number;
            }
            catch
            {

            }
        }
        /// <summary>
        /// Change the status of the form
        /// </summary>
        /// <param name="status">Status</param>
        public void SetFormStatus(FormBindingStatus status)
        {
            switch (status)
            {
                case FormBindingStatus.Waiting:
                    mibStatusPicture.Visible = false;
                    break;
                case FormBindingStatus.Adding:
                    mibStatusPicture.Visible = true;
                    mibStatusPicture.Image = global::Net4Sage.Properties.Resources.add_rec.ToBitmap();
                    break;
                case FormBindingStatus.Editing:
                    mibStatusPicture.Visible = true;
                    mibStatusPicture.Image = global::Net4Sage.Properties.Resources.edit_record.ToBitmap();
                    break;
                default:
                    mibStatusPicture.Visible = false;
                    break;
            }
        }
        private void UpdateSessionData(object sender, EventArgs e)
        {
            lblBusDate.Text = SysSession.BusinessDate.ToString("dd-MM-yyyy");
            lblUserID.Text = SysSession.UserID;
            lblCompanyID.Text = SysSession.CompanyID;
        }
    }
}
