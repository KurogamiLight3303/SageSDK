using CrystalDecisions.CrystalReports.Engine;
using Net4Sage;
using Net4Sage.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportEngine
{
    /// <summary>
    /// Report Control
    /// </summary>
    public partial class CrystalReportEngine : Component
    {
        private CrystalReportForm ReportForm { get; set; }
        /// <summary>
        /// Create Intance of the control
        /// </summary>
        public CrystalReportEngine()
        {
            InitializeComponent();
        }
        /// <summary>
        /// Create Intance of the control
        /// </summary>
        /// <param name="container"></param>
        public CrystalReportEngine(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
        /// <summary>
        /// Reload Report
        /// </summary>
        public void Reload()
        {
            if (Report != null)
            {
                Report.Load();
            }
        }
        /// <summary>
        /// Show window
        /// </summary>
        public void Show()
        {
            ShowReport();
            ReportForm.Show();
        }
        /// <summary>
        /// Show in a modal window
        /// </summary>
        public void ShowDialog()
        {
            ShowReport();
            ReportForm.ShowDialog();
        }
        private void ShowReport()
        {
            if (SysSession != null && Report != null)
                SysSession.LogonSource(Report);
            ReportForm = new CrystalReportForm();
            ReportForm.ReportViewer.ReportSource = Report;
            string src;
            byte[] logo;
            try
            {
                src = @"%ProgramData%\Sage Software\Sage MAS 500";
                src = Environment.ExpandEnvironmentVariables(src);

                if (!Directory.Exists(src))
                    Directory.CreateDirectory(src);
                logo = SysSession.Lookup<byte[]>("t.BlobObject", "tsmGlobalRptFormat as p join tsmGlobalRptFormatDetail as s on p.GlobalRptFormatKey = s.GlobalRptFormatKey join tsmBlob as t on s.ImageBlobKey = t.BlobKey",
                    "s.SectionType = 10 and (p.CompanyID is null or p.CompanyID = " + SageSession.QuoteString(SysSession.CompanyID) + ") order by p.CompanyID");
                if(logo != null)
                {
                    FileStream fileStream = new FileStream(src + "\\Logo.jpg", FileMode.OpenOrCreate, FileAccess.Write);
                    fileStream.Write(logo, 0, logo.Length);
                    fileStream.Close();
                    Report.DataDefinition.FormulaFields["LogoSrc"].Text = "\"" + src + "\\Logo.jpg \"";
                }
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc);
            }

            try
            {
                Report.ReportDefinition.ReportObjects["Logo"].Width = LogoSize.Width;
                Report.ReportDefinition.ReportObjects["Logo"].Height = LogoSize.Height;
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc);
            }

            try
            {
                Report.DataDefinition.FormulaFields["ReportTitle"].Text = "\"" + this.Title + "\"";
            }
            catch (Exception exc)
            {
                SysSession.WriteLog(exc);
            }
        }
        /// <summary>
        /// Login Database source
        /// </summary>
        /// <param name="tableIndex">table index</param>
        public void LoginDataSource(int tableIndex)
        {
            if (SysSession != null && Report != null)
            {
                if (Report.Database.Tables.Count > tableIndex)
                {
                    SysSession.LogonSource(Report.Database.Tables[tableIndex]);
                }
            }
        }
        /// <summary>
        /// Sage Session
        /// </summary>
        public SageSession SysSession { get; set; }
        /// <summary>
        /// Crystal Report 
        /// </summary>
        public ReportClass Report { get; set; }
        /// <summary>
        /// Size of the logo
        /// </summary>
        public Size LogoSize { get; set; }
        /// <summary>
        /// Title of the report
        /// </summary>
        public string Title { get; set; }

    }
}
