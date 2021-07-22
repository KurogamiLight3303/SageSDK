namespace Net4Sage.Controls.Lookup
{
    partial class GLAccountLookup
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
            this.pnlLookup = new System.Windows.Forms.Panel();
            this.lkuAccountLookup = new Net4Sage.Controls.Lookup.TextLookup();
            this.pnlDescription = new System.Windows.Forms.Panel();
            this.lblAccountDescription = new System.Windows.Forms.Label();
            this.pnlLookup.SuspendLayout();
            this.pnlDescription.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlLookup
            // 
            this.pnlLookup.Controls.Add(this.lkuAccountLookup);
            this.pnlLookup.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLookup.Location = new System.Drawing.Point(0, 0);
            this.pnlLookup.Name = "pnlLookup";
            this.pnlLookup.Size = new System.Drawing.Size(195, 32);
            this.pnlLookup.TabIndex = 0;
            // 
            // lkuAccountLookup
            // 
            this.lkuAccountLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lkuAccountLookup.Enabled = false;
            this.lkuAccountLookup.ErrorMessage = null;
            this.lkuAccountLookup.Key = 0;
            this.lkuAccountLookup.Location = new System.Drawing.Point(0, 0);
            this.lkuAccountLookup.Mask = "";
            this.lkuAccountLookup.MinimumSize = new System.Drawing.Size(27, 23);
            this.lkuAccountLookup.Name = "lkuAccountLookup";
            this.lkuAccountLookup.Protected = false;
            this.lkuAccountLookup.Size = new System.Drawing.Size(195, 32);
            this.lkuAccountLookup.SysSession = null;
            this.lkuAccountLookup.TabIndex = 0;
            this.lkuAccountLookup.TextOnlyLookup = false;
            this.lkuAccountLookup.OnValueChange += new System.EventHandler(this.On_Account_Change);
            // 
            // pnlDescription
            // 
            this.pnlDescription.Controls.Add(this.lblAccountDescription);
            this.pnlDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDescription.Location = new System.Drawing.Point(195, 0);
            this.pnlDescription.Name = "pnlDescription";
            this.pnlDescription.Size = new System.Drawing.Size(253, 32);
            this.pnlDescription.TabIndex = 1;
            // 
            // lblAccountDescription
            // 
            this.lblAccountDescription.AutoSize = true;
            this.lblAccountDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblAccountDescription.Location = new System.Drawing.Point(0, 0);
            this.lblAccountDescription.Name = "lblAccountDescription";
            this.lblAccountDescription.Size = new System.Drawing.Size(0, 13);
            this.lblAccountDescription.TabIndex = 0;
            // 
            // GLAccountLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlDescription);
            this.Controls.Add(this.pnlLookup);
            this.Name = "GLAccountLookup";
            this.Size = new System.Drawing.Size(448, 32);
            this.pnlLookup.ResumeLayout(false);
            this.pnlDescription.ResumeLayout(false);
            this.pnlDescription.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlLookup;
        private System.Windows.Forms.Panel pnlDescription;
        private TextLookup lkuAccountLookup;
        private System.Windows.Forms.Label lblAccountDescription;
    }
}
