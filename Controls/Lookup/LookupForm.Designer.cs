namespace Net4Sage.Controls.Lookup
{
    partial class LookupForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LookupForm));
            this.tbrMain = new System.Windows.Forms.MenuStrip();
            this.mibFinish = new System.Windows.Forms.ToolStripMenuItem();
            this.mibCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.mibFilter = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlFilters = new System.Windows.Forms.Panel();
            this.fraFilters = new System.Windows.Forms.GroupBox();
            this.grdFilters = new Net4Sage.Controls.GridSelect.GridSelect();
            this.pnlData = new System.Windows.Forms.Panel();
            this.fraData = new System.Windows.Forms.GroupBox();
            this.grdData = new System.Windows.Forms.DataGridView();
            this.dataBS = new System.Windows.Forms.BindingSource(this.components);
            this.statusBar1 = new Net4Sage.Controls.StatusBar();
            this.GridNav = new Net4Sage.Controls.Navigators.GridNavigator(this.components);
            this.SysSession = new Net4Sage.SageSession();
            this.tbrMain.SuspendLayout();
            this.pnlFilters.SuspendLayout();
            this.fraFilters.SuspendLayout();
            this.pnlData.SuspendLayout();
            this.fraData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataBS)).BeginInit();
            this.SuspendLayout();
            // 
            // tbrMain
            // 
            this.tbrMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mibFinish,
            this.mibCancel,
            this.mibFilter});
            this.tbrMain.Location = new System.Drawing.Point(0, 0);
            this.tbrMain.Name = "tbrMain";
            this.tbrMain.Size = new System.Drawing.Size(803, 24);
            this.tbrMain.TabIndex = 0;
            this.tbrMain.Text = "menuStrip1";
            // 
            // mibFinish
            // 
            this.mibFinish.Image = global::Net4Sage.Properties.Resources.OK_16;
            this.mibFinish.Name = "mibFinish";
            this.mibFinish.Size = new System.Drawing.Size(28, 20);
            this.mibFinish.ToolTipText = "Aceptar";
            this.mibFinish.Click += new System.EventHandler(this.Do_Finish);
            // 
            // mibCancel
            // 
            this.mibCancel.Image = global::Net4Sage.Properties.Resources.cancl_16;
            this.mibCancel.Name = "mibCancel";
            this.mibCancel.Size = new System.Drawing.Size(28, 20);
            this.mibCancel.ToolTipText = "Cancelar";
            this.mibCancel.Click += new System.EventHandler(this.Do_Cancel);
            // 
            // mibFilter
            // 
            this.mibFilter.Image = global::Net4Sage.Properties.Resources.Refresh_16;
            this.mibFilter.Name = "mibFilter";
            this.mibFilter.Size = new System.Drawing.Size(28, 20);
            this.mibFilter.ToolTipText = "Filtrar";
            this.mibFilter.Click += new System.EventHandler(this.Do_Filtrate);
            // 
            // pnlFilters
            // 
            this.pnlFilters.Controls.Add(this.fraFilters);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.Location = new System.Drawing.Point(0, 24);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFilters.Size = new System.Drawing.Size(803, 208);
            this.pnlFilters.TabIndex = 1;
            // 
            // fraFilters
            // 
            this.fraFilters.Controls.Add(this.grdFilters);
            this.fraFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraFilters.Location = new System.Drawing.Point(10, 10);
            this.fraFilters.Name = "fraFilters";
            this.fraFilters.Padding = new System.Windows.Forms.Padding(5);
            this.fraFilters.Size = new System.Drawing.Size(783, 188);
            this.fraFilters.TabIndex = 0;
            this.fraFilters.TabStop = false;
            this.fraFilters.Text = "Filtros";
            // 
            // grdFilters
            // 
            this.grdFilters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdFilters.Location = new System.Drawing.Point(5, 18);
            this.grdFilters.Name = "grdFilters";
            this.grdFilters.Padding = new System.Windows.Forms.Padding(5);
            this.grdFilters.Size = new System.Drawing.Size(773, 165);
            this.grdFilters.TabIndex = 0;
            // 
            // pnlData
            // 
            this.pnlData.Controls.Add(this.fraData);
            this.pnlData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlData.Location = new System.Drawing.Point(0, 232);
            this.pnlData.Name = "pnlData";
            this.pnlData.Padding = new System.Windows.Forms.Padding(10, 10, 10, 40);
            this.pnlData.Size = new System.Drawing.Size(803, 278);
            this.pnlData.TabIndex = 2;
            // 
            // fraData
            // 
            this.fraData.Controls.Add(this.grdData);
            this.fraData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fraData.Location = new System.Drawing.Point(10, 10);
            this.fraData.Name = "fraData";
            this.fraData.Padding = new System.Windows.Forms.Padding(5);
            this.fraData.Size = new System.Drawing.Size(783, 228);
            this.fraData.TabIndex = 0;
            this.fraData.TabStop = false;
            this.fraData.Text = "Datos";
            // 
            // grdData
            // 
            this.grdData.AllowUserToAddRows = false;
            this.grdData.AllowUserToDeleteRows = false;
            this.grdData.AllowUserToOrderColumns = true;
            this.grdData.AutoGenerateColumns = false;
            this.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdData.DataSource = this.dataBS;
            this.grdData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdData.Location = new System.Drawing.Point(5, 18);
            this.grdData.Name = "grdData";
            this.grdData.ReadOnly = true;
            this.grdData.Size = new System.Drawing.Size(773, 205);
            this.grdData.TabIndex = 0;
            this.grdData.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.On_DataRow_Added);
            this.grdData.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Do_Click_Select);
            // 
            // statusBar1
            // 
            this.statusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.statusBar1.Location = new System.Drawing.Point(0, 479);
            this.statusBar1.MinimumSize = new System.Drawing.Size(0, 31);
            this.statusBar1.Name = "statusBar1";
            this.statusBar1.Navigator = this.GridNav;
            this.statusBar1.Padding = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.statusBar1.Size = new System.Drawing.Size(803, 31);
            this.statusBar1.SysSession = this.SysSession;
            this.statusBar1.TabIndex = 3;
            // 
            // GridNav
            // 
            this.GridNav.BindingSource = this.dataBS;
            this.GridNav.LoadFull = false;
            this.GridNav.PageSize = 10;
            // 
            // SysSession
            // 
            this.SysSession.Parameters = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("SysSession.Parameters")));
            // 
            // LookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(803, 510);
            this.Controls.Add(this.statusBar1);
            this.Controls.Add(this.pnlData);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.tbrMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.tbrMain;
            this.Name = "LookupForm";
            this.Text = "Lookup";
            this.Load += new System.EventHandler(this.Form_Load);
            this.tbrMain.ResumeLayout(false);
            this.tbrMain.PerformLayout();
            this.pnlFilters.ResumeLayout(false);
            this.fraFilters.ResumeLayout(false);
            this.pnlData.ResumeLayout(false);
            this.fraData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataBS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip tbrMain;
        private System.Windows.Forms.ToolStripMenuItem mibFinish;
        private System.Windows.Forms.ToolStripMenuItem mibCancel;
        private System.Windows.Forms.ToolStripMenuItem mibFilter;
        private System.Windows.Forms.Panel pnlFilters;
        private System.Windows.Forms.GroupBox fraFilters;
        private System.Windows.Forms.Panel pnlData;
        private System.Windows.Forms.GroupBox fraData;
        private System.Windows.Forms.DataGridView grdData;
        private System.Windows.Forms.BindingSource dataBS;
        private GridSelect.GridSelect grdFilters;
        private StatusBar statusBar1;
        private Navigators.GridNavigator GridNav;
        internal SageSession SysSession;
    }
}