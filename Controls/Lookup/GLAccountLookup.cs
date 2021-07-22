using Net4Sage.Model;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Net4Sage.Controls.Lookup
{
    /// <summary>
    /// Control for GLAccount Lookup
    /// </summary>
    public partial class GLAccountLookup : UserControl
    {
        /// <summary>
        /// The Key of the Account Selected
        /// </summary>
        [Bindable(true)]
        public int Key { get => lkuAccountLookup.Key; set => lkuAccountLookup.Key = value; }
        /// <summary>
        /// The Account Number
        /// </summary>
        [Bindable(true)]
        public string Text { get => lkuAccountLookup.Text; set => lkuAccountLookup.Text = value; }
        /// <summary>
        /// If true only show the accounts active in the company
        /// </summary>
        public bool OnlyActiveAccounts { get; set; }
        /// <summary>
        /// Enable Component
        /// </summary>
        public bool Enable { get => lkuAccountLookup.Enabled; set => lkuAccountLookup.Enabled = value; }
        /// <summary>
        /// Sage Session
        /// </summary>
        public SageSession SysSession
        {
            get => lkuAccountLookup.SysSession;
            set
            {
                if (lkuAccountLookup.SysSession != value)
                {
                    lkuAccountLookup.SysSession = value;
                    lkuAccountLookup.SysSession.OnSessionUpdate += LoadAccounts;
                }
            }
        }
        /// <summary>
        /// Create instance of the Control
        /// </summary>
        public GLAccountLookup()
        {
            InitializeComponent();
        }
        private void On_Account_Change(object sender, EventArgs e)
        {
            GLAccountEntry entry;
            if (lkuAccountLookup.Key > 0 && (entry = lkuAccountLookup.GetObjectByID(lkuAccountLookup.Text) as GLAccountEntry) != null)
                lblAccountDescription.Text = entry.Description;
            else
                lblAccountDescription.Text = "";
        }
        private void LoadAccounts(object sender, EventArgs e)
        {
            lkuAccountLookup.SetData(SysSession.LookupObjectList<GLAccountEntry>("vluAccount", "CompanyID =" + SageSession.QuoteString(SysSession.CompanyID) + (OnlyActiveAccounts ? " and Status = 1" : "") + " order by GLAcctNo"));
            lkuAccountLookup.Mask = SysSession.Lookup<string>("AcctMask", "tglOptions", "CompanyID =" + SageSession.QuoteString(SysSession.CompanyID)).Replace('N', 'A');
            lkuAccountLookup.Enabled = true;
        }
    }
}
