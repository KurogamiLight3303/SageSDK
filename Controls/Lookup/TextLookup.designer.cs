namespace Net4Sage.Controls.Lookup
{
    partial class TextLookup
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
            this.pnlBtn = new System.Windows.Forms.Panel();
            this.lkuBtn = new Net4Sage.Controls.Lookup.LookupButton();
            this.pnlText = new System.Windows.Forms.Panel();
            this.lkuText = new System.Windows.Forms.MaskedTextBox();
            this.pnlBtn.SuspendLayout();
            this.pnlText.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBtn
            // 
            this.pnlBtn.Controls.Add(this.lkuBtn);
            this.pnlBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBtn.Location = new System.Drawing.Point(113, 0);
            this.pnlBtn.MaximumSize = new System.Drawing.Size(27, 23);
            this.pnlBtn.MinimumSize = new System.Drawing.Size(27, 23);
            this.pnlBtn.Name = "pnlBtn";
            this.pnlBtn.Padding = new System.Windows.Forms.Padding(2, 0, 0, 0);
            this.pnlBtn.Size = new System.Drawing.Size(27, 23);
            this.pnlBtn.TabIndex = 3;
            // 
            // lkuBtn
            // 
            this.lkuBtn.BackColor = System.Drawing.Color.Transparent;
            this.lkuBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lkuBtn.Enable = true;
            this.lkuBtn.Location = new System.Drawing.Point(2, 0);
            this.lkuBtn.Name = "lkuBtn";
            this.lkuBtn.Padding = new System.Windows.Forms.Padding(1);
            this.lkuBtn.Size = new System.Drawing.Size(25, 23);
            this.lkuBtn.SysSession = null;
            this.lkuBtn.TabIndex = 2;
            // 
            // pnlText
            // 
            this.pnlText.Controls.Add(this.lkuText);
            this.pnlText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlText.Location = new System.Drawing.Point(0, 0);
            this.pnlText.Name = "pnlText";
            this.pnlText.Padding = new System.Windows.Forms.Padding(1);
            this.pnlText.Size = new System.Drawing.Size(113, 23);
            this.pnlText.TabIndex = 4;
            // 
            // lkuText
            // 
            this.lkuText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lkuText.Location = new System.Drawing.Point(1, 1);
            this.lkuText.Name = "lkuText";
            this.lkuText.Size = new System.Drawing.Size(111, 20);
            this.lkuText.TabIndex = 3;
            this.lkuText.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.lkuText.Leave += new System.EventHandler(this.Do_TextInput_Leave);
            this.lkuText.Validating += new System.ComponentModel.CancelEventHandler(this.Validate_Text);
            // 
            // TextLookup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlText);
            this.Controls.Add(this.pnlBtn);
            this.MinimumSize = new System.Drawing.Size(27, 23);
            this.Name = "TextLookup";
            this.Size = new System.Drawing.Size(140, 23);
            this.Leave += new System.EventHandler(this.Do_TextInput_Leave);
            this.Validating += new System.ComponentModel.CancelEventHandler(this.Validate_Text);
            this.pnlBtn.ResumeLayout(false);
            this.pnlText.ResumeLayout(false);
            this.pnlText.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlBtn;
        private LookupButton lkuBtn;
        private System.Windows.Forms.Panel pnlText;
        private System.Windows.Forms.MaskedTextBox lkuText;
    }
}
