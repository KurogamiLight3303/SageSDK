namespace Net4Sage.CIUtils
{
    partial class CommonRegistrationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonRegistrationForm));
            this.grbRegisterOptions = new System.Windows.Forms.GroupBox();
            this.chkPostCheck = new System.Windows.Forms.CheckBox();
            this.chkPost = new System.Windows.Forms.CheckBox();
            this.cbxOutput = new System.Windows.Forms.ComboBox();
            this.lbl = new System.Windows.Forms.Label();
            this.chkRegister = new System.Windows.Forms.CheckBox();
            this.MainMenu = new Net4Sage.Controls.MenuBar();
            this.SysSession = new Net4Sage.SageSession();
            this.strStatusBar = new Net4Sage.Controls.StatusBar();
            this.grbRegisterOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // grbRegisterOptions
            // 
            this.grbRegisterOptions.Controls.Add(this.chkPostCheck);
            this.grbRegisterOptions.Controls.Add(this.chkPost);
            this.grbRegisterOptions.Controls.Add(this.cbxOutput);
            this.grbRegisterOptions.Controls.Add(this.lbl);
            this.grbRegisterOptions.Controls.Add(this.chkRegister);
            this.grbRegisterOptions.Location = new System.Drawing.Point(24, 34);
            this.grbRegisterOptions.Name = "grbRegisterOptions";
            this.grbRegisterOptions.Size = new System.Drawing.Size(274, 143);
            this.grbRegisterOptions.TabIndex = 0;
            this.grbRegisterOptions.TabStop = false;
            // 
            // chkPostCheck
            // 
            this.chkPostCheck.AutoSize = true;
            this.chkPostCheck.Location = new System.Drawing.Point(46, 94);
            this.chkPostCheck.Name = "chkPostCheck";
            this.chkPostCheck.Size = new System.Drawing.Size(151, 17);
            this.chkPostCheck.TabIndex = 4;
            this.chkPostCheck.Text = "Preguntar antes de Aplicar";
            this.chkPostCheck.UseVisualStyleBackColor = true;
            // 
            // chkPost
            // 
            this.chkPost.AutoSize = true;
            this.chkPost.Location = new System.Drawing.Point(30, 71);
            this.chkPost.Name = "chkPost";
            this.chkPost.Size = new System.Drawing.Size(58, 17);
            this.chkPost.TabIndex = 3;
            this.chkPost.Text = "Aplicar";
            this.chkPost.UseVisualStyleBackColor = true;
            this.chkPost.CheckedChanged += new System.EventHandler(this.On_CheckedChange);
            // 
            // cbxOutput
            // 
            this.cbxOutput.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxOutput.FormattingEnabled = true;
            this.cbxOutput.Items.AddRange(new object[] {
            "Pantalla"});
            this.cbxOutput.Location = new System.Drawing.Point(85, 50);
            this.cbxOutput.Name = "cbxOutput";
            this.cbxOutput.Size = new System.Drawing.Size(123, 21);
            this.cbxOutput.TabIndex = 2;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Location = new System.Drawing.Point(43, 53);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(36, 13);
            this.lbl.TabIndex = 1;
            this.lbl.Text = "Salida";
            // 
            // chkRegister
            // 
            this.chkRegister.AutoSize = true;
            this.chkRegister.Location = new System.Drawing.Point(30, 30);
            this.chkRegister.Name = "chkRegister";
            this.chkRegister.Size = new System.Drawing.Size(68, 17);
            this.chkRegister.TabIndex = 0;
            this.chkRegister.Text = "Registrar";
            this.chkRegister.UseVisualStyleBackColor = true;
            this.chkRegister.CheckedChanged += new System.EventHandler(this.On_Register_CheckedChange);
            // 
            // MainMenu
            // 
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Mode = Net4Sage.Controls.MenuBarMode.Operation;
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.ShowItemToolTips = true;
            this.MainMenu.Size = new System.Drawing.Size(326, 24);
            this.MainMenu.SysSession = this.SysSession;
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuBar1";
            // 
            // SysSession
            // 
            this.SysSession.Parameters = ((System.Collections.Generic.Dictionary<string, string>)(resources.GetObject("SysSession.Parameters")));
            // 
            // strStatusBar
            // 
            this.strStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.strStatusBar.Location = new System.Drawing.Point(0, 195);
            this.strStatusBar.MinimumSize = new System.Drawing.Size(0, 31);
            this.strStatusBar.Name = "strStatusBar";
            this.strStatusBar.Padding = new System.Windows.Forms.Padding(0, 3, 5, 3);
            this.strStatusBar.Size = new System.Drawing.Size(326, 31);
            this.strStatusBar.SysSession = this.SysSession;
            this.strStatusBar.TabIndex = 2;
            // 
            // CommonRegistrationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 226);
            this.Controls.Add(this.strStatusBar);
            this.Controls.Add(this.grbRegisterOptions);
            this.Controls.Add(this.MainMenu);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "CommonRegistrationForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar";
            this.grbRegisterOptions.ResumeLayout(false);
            this.grbRegisterOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grbRegisterOptions;
        private System.Windows.Forms.CheckBox chkRegister;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.ComboBox cbxOutput;
        private System.Windows.Forms.CheckBox chkPost;
        private System.Windows.Forms.CheckBox chkPostCheck;
        private Controls.StatusBar strStatusBar;
        private SageSession SysSession;
        public Controls.MenuBar MainMenu;
    }
}