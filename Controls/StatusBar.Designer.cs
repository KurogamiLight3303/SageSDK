namespace Net4Sage.Controls
{
    partial class StatusBar
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.sbrStatus = new System.Windows.Forms.StatusStrip();
            this.lblBusDate = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUserID = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCompanyID = new System.Windows.Forms.ToolStripStatusLabel();
            this.mibStatusPicture = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.nvcController = new Net4Sage.Controls.Navigators.NavigatorController();
            this.sbrStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // sbrStatus
            // 
            this.sbrStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sbrStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblBusDate,
            this.lblUserID,
            this.lblCompanyID,
            this.mibStatusPicture,
            this.toolStripProgressBar1});
            this.sbrStatus.Location = new System.Drawing.Point(0, 3);
            this.sbrStatus.Name = "sbrStatus";
            this.sbrStatus.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.sbrStatus.Size = new System.Drawing.Size(814, 25);
            this.sbrStatus.TabIndex = 0;
            this.sbrStatus.Text = "statusStrip1";
            // 
            // lblBusDate
            // 
            this.lblBusDate.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblBusDate.Name = "lblBusDate";
            this.lblBusDate.Size = new System.Drawing.Size(4, 20);
            // 
            // lblUserID
            // 
            this.lblUserID.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblUserID.Name = "lblUserID";
            this.lblUserID.Size = new System.Drawing.Size(4, 20);
            // 
            // lblCompanyID
            // 
            this.lblCompanyID.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.lblCompanyID.Name = "lblCompanyID";
            this.lblCompanyID.Size = new System.Drawing.Size(4, 20);
            // 
            // mibStatusPicture
            // 
            this.mibStatusPicture.Name = "mibStatusPicture";
            this.mibStatusPicture.Size = new System.Drawing.Size(0, 20);
            this.mibStatusPicture.Visible = false;
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 19);
            this.toolStripProgressBar1.Visible = false;
            // 
            // nvcController
            // 
            this.nvcController.BackColor = System.Drawing.Color.Transparent;
            this.nvcController.Location = new System.Drawing.Point(18, 4);
            this.nvcController.MaximumSize = new System.Drawing.Size(200, 30);
            this.nvcController.MinimumSize = new System.Drawing.Size(200, 30);
            this.nvcController.Name = "nvcController";
            this.nvcController.Navigator = null;
            this.nvcController.Size = new System.Drawing.Size(200, 30);
            this.nvcController.TabIndex = 1;
            this.nvcController.Visible = false;
            // 
            // StatusBar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nvcController);
            this.Controls.Add(this.sbrStatus);
            this.MinimumSize = new System.Drawing.Size(0, 31);
            this.Name = "StatusBar";
            this.Padding = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.Size = new System.Drawing.Size(819, 31);
            this.sbrStatus.ResumeLayout(false);
            this.sbrStatus.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip sbrStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblBusDate;
        private System.Windows.Forms.ToolStripStatusLabel lblUserID;
        private System.Windows.Forms.ToolStripStatusLabel lblCompanyID;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel mibStatusPicture;
        private Navigators.NavigatorController nvcController;
    }
}
