namespace Net4Sage.Controls.DropDown
{
    partial class DynamicDropDown
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
            this.components = new System.ComponentModel.Container();
            this.cbxComboBox = new System.Windows.Forms.ComboBox();
            this.MainBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainBS)).BeginInit();
            this.SuspendLayout();
            // 
            // cbxComboBox
            // 
            this.cbxComboBox.DataSource = this.MainBS;
            this.cbxComboBox.DisplayMember = "Value";
            this.cbxComboBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cbxComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxComboBox.FormattingEnabled = true;
            this.cbxComboBox.Location = new System.Drawing.Point(0, 0);
            this.cbxComboBox.Name = "cbxComboBox";
            this.cbxComboBox.Size = new System.Drawing.Size(150, 21);
            this.cbxComboBox.TabIndex = 1;
            this.cbxComboBox.ValueMember = "Key";
            // 
            // MainBS
            // 
            this.MainBS.DataSource = typeof(System.Collections.Generic.KeyValuePair<int, string>);
            // 
            // DynamicDropDown
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbxComboBox);
            this.Name = "DynamicDropDown";
            this.Size = new System.Drawing.Size(150, 23);
            ((System.ComponentModel.ISupportInitialize)(this.MainBS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbxComboBox;
        private System.Windows.Forms.BindingSource MainBS;
    }
}
