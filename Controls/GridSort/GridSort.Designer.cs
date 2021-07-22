namespace Net4Sage.Controls.GridSort
{
    partial class GridSort
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
            this.grdRows = new System.Windows.Forms.DataGridView();
            this.fieldDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.groupByDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.subtotalizeDataGridViewCheckBoxColumn = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SortRowsBS = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grdRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SortRowsBS)).BeginInit();
            this.SuspendLayout();
            // 
            // grdRows
            // 
            this.grdRows.AutoGenerateColumns = false;
            this.grdRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.fieldDataGridViewTextBoxColumn,
            this.groupByDataGridViewCheckBoxColumn,
            this.subtotalizeDataGridViewCheckBoxColumn});
            this.grdRows.DataSource = this.SortRowsBS;
            this.grdRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRows.Location = new System.Drawing.Point(0, 0);
            this.grdRows.Name = "grdRows";
            this.grdRows.Size = new System.Drawing.Size(739, 339);
            this.grdRows.TabIndex = 0;
            this.grdRows.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.OnCellValidating);
            this.grdRows.UserAddedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.On_RowCount_Change);
            this.grdRows.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.On_RowCount_Change);
            // 
            // fieldDataGridViewTextBoxColumn
            // 
            this.fieldDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.fieldDataGridViewTextBoxColumn.DataPropertyName = "Field";
            this.fieldDataGridViewTextBoxColumn.HeaderText = "Campo";
            this.fieldDataGridViewTextBoxColumn.Name = "fieldDataGridViewTextBoxColumn";
            this.fieldDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.fieldDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            // 
            // groupByDataGridViewCheckBoxColumn
            // 
            this.groupByDataGridViewCheckBoxColumn.DataPropertyName = "GroupBy";
            this.groupByDataGridViewCheckBoxColumn.HeaderText = "Agrupar";
            this.groupByDataGridViewCheckBoxColumn.Name = "groupByDataGridViewCheckBoxColumn";
            // 
            // subtotalizeDataGridViewCheckBoxColumn
            // 
            this.subtotalizeDataGridViewCheckBoxColumn.DataPropertyName = "Subtotalize";
            this.subtotalizeDataGridViewCheckBoxColumn.HeaderText = "Subtotalizar";
            this.subtotalizeDataGridViewCheckBoxColumn.Name = "subtotalizeDataGridViewCheckBoxColumn";
            // 
            // SortRowsBS
            // 
            this.SortRowsBS.DataSource = typeof(Net4Sage.Controls.GridSort.SortRow);
            // 
            // GridSort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdRows);
            this.Name = "GridSort";
            this.Size = new System.Drawing.Size(739, 339);
            ((System.ComponentModel.ISupportInitialize)(this.grdRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SortRowsBS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdRows;
        private System.Windows.Forms.BindingSource SortRowsBS;
        private System.Windows.Forms.DataGridViewComboBoxColumn fieldDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn groupByDataGridViewCheckBoxColumn;
        private System.Windows.Forms.DataGridViewCheckBoxColumn subtotalizeDataGridViewCheckBoxColumn;
    }
}
