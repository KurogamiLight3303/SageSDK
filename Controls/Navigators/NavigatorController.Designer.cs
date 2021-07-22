namespace Net4Sage.Controls.Navigators
{
    partial class NavigatorController
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
            this.tlpNavigator = new System.Windows.Forms.TableLayoutPanel();
            this.btnPrevious = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.lblNavigator = new System.Windows.Forms.Label();
            this.tlpNavigator.SuspendLayout();
            this.SuspendLayout();
            // 
            // tlpNavigator
            // 
            this.tlpNavigator.BackColor = System.Drawing.Color.Transparent;
            this.tlpNavigator.ColumnCount = 5;
            this.tlpNavigator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpNavigator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpNavigator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tlpNavigator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tlpNavigator.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tlpNavigator.Controls.Add(this.btnPrevious, 0, 0);
            this.tlpNavigator.Controls.Add(this.btnFirst, 0, 0);
            this.tlpNavigator.Controls.Add(this.btnNext, 3, 0);
            this.tlpNavigator.Controls.Add(this.btnEnd, 4, 0);
            this.tlpNavigator.Controls.Add(this.lblNavigator, 2, 0);
            this.tlpNavigator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpNavigator.Location = new System.Drawing.Point(0, 0);
            this.tlpNavigator.Name = "tlpNavigator";
            this.tlpNavigator.RowCount = 1;
            this.tlpNavigator.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpNavigator.Size = new System.Drawing.Size(200, 28);
            this.tlpNavigator.TabIndex = 3;
            // 
            // btnPrevious
            // 
            this.btnPrevious.BackgroundImage = global::Net4Sage.Properties.Resources.Browse_Previous_16;
            this.btnPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnPrevious.Location = new System.Drawing.Point(33, 3);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(24, 22);
            this.btnPrevious.TabIndex = 1;
            this.btnPrevious.UseVisualStyleBackColor = true;
            this.btnPrevious.Click += new System.EventHandler(this.btnPrevious_Click);
            // 
            // btnFirst
            // 
            this.btnFirst.BackgroundImage = global::Net4Sage.Properties.Resources.Start_16;
            this.btnFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnFirst.Location = new System.Drawing.Point(3, 3);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(24, 22);
            this.btnFirst.TabIndex = 0;
            this.btnFirst.UseVisualStyleBackColor = true;
            this.btnFirst.Click += new System.EventHandler(this.btnFirst_Click);
            // 
            // btnNext
            // 
            this.btnNext.BackgroundImage = global::Net4Sage.Properties.Resources.Browse_Next_16;
            this.btnNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnNext.Location = new System.Drawing.Point(133, 3);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(24, 22);
            this.btnNext.TabIndex = 2;
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.BackgroundImage = global::Net4Sage.Properties.Resources.End_16;
            this.btnEnd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnEnd.Location = new System.Drawing.Point(163, 3);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(24, 22);
            this.btnEnd.TabIndex = 3;
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // lblNavigator
            // 
            this.lblNavigator.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNavigator.AutoSize = true;
            this.lblNavigator.Location = new System.Drawing.Point(63, 0);
            this.lblNavigator.Name = "lblNavigator";
            this.lblNavigator.Size = new System.Drawing.Size(64, 28);
            this.lblNavigator.TabIndex = 4;
            this.lblNavigator.Text = "-- / --";
            this.lblNavigator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // NavigatorController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tlpNavigator);
            this.MaximumSize = new System.Drawing.Size(200, 28);
            this.MinimumSize = new System.Drawing.Size(200, 28);
            this.Name = "NavigatorController";
            this.Size = new System.Drawing.Size(200, 28);
            this.tlpNavigator.ResumeLayout(false);
            this.tlpNavigator.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpNavigator;
        private System.Windows.Forms.Button btnPrevious;
        private System.Windows.Forms.Button btnFirst;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnEnd;
        public System.Windows.Forms.Label lblNavigator;
    }
}
