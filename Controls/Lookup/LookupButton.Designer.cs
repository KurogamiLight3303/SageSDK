namespace Net4Sage.Controls.Lookup
{
    partial class LookupButton
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
            this.btnLookup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnLookup
            // 
            this.btnLookup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnLookup.Image = global::Net4Sage.Properties.Resources.srch_16;
            this.btnLookup.Location = new System.Drawing.Point(0, 0);
            this.btnLookup.Name = "btnLookup";
            this.btnLookup.Size = new System.Drawing.Size(23, 23);
            this.btnLookup.TabIndex = 0;
            this.btnLookup.UseVisualStyleBackColor = true;
            this.btnLookup.Click += new System.EventHandler(this.Open_Form);
            // 
            // LookupButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnLookup);
            this.Name = "LookupButton";
            this.Size = new System.Drawing.Size(23, 23);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnLookup;
    }
}
