namespace Net4Sage.Controls.GridSelect
{
    partial class GridSelect
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grdRows = new System.Windows.Forms.DataGridView();
            this.TypeBS = new System.Windows.Forms.BindingSource(this.components);
            this.SelectRowBS = new System.Windows.Forms.BindingSource(this.components);
            this.cmsMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.añadirFiltroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eliminarFiltroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headerDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kColFilter = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.kColValue1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.kColValue2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.grdRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeBS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectRowBS)).BeginInit();
            this.cmsMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdRows
            // 
            this.grdRows.AllowUserToAddRows = false;
            this.grdRows.AllowUserToDeleteRows = false;
            this.grdRows.AutoGenerateColumns = false;
            this.grdRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grdRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.headerDataGridViewTextBoxColumn,
            this.kColFilter,
            this.kColValue1,
            this.kColValue2});
            this.grdRows.DataSource = this.SelectRowBS;
            this.grdRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRows.Location = new System.Drawing.Point(5, 5);
            this.grdRows.MultiSelect = false;
            this.grdRows.Name = "grdRows";
            this.grdRows.Size = new System.Drawing.Size(534, 250);
            this.grdRows.TabIndex = 0;
            this.grdRows.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.Cell_Click);
            this.grdRows.CellLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.On_Cell_Leaves);
            this.grdRows.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.On_Control_Showing);
            // 
            // TypeBS
            // 
            this.TypeBS.AllowNew = false;
            this.TypeBS.DataSource = typeof(Net4Sage.Controls.GridSelect.SelectRowTypeEntry);
            // 
            // SelectRowBS
            // 
            this.SelectRowBS.DataSource = typeof(Net4Sage.Controls.GridSelect.SelectRow);
            this.SelectRowBS.Sort = "";
            // 
            // cmsMenu
            // 
            this.cmsMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.añadirFiltroToolStripMenuItem,
            this.eliminarFiltroToolStripMenuItem});
            this.cmsMenu.Name = "cmsMenu";
            this.cmsMenu.Size = new System.Drawing.Size(148, 48);
            // 
            // añadirFiltroToolStripMenuItem
            // 
            this.añadirFiltroToolStripMenuItem.Name = "añadirFiltroToolStripMenuItem";
            this.añadirFiltroToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.añadirFiltroToolStripMenuItem.Text = "Añadir Filtro";
            this.añadirFiltroToolStripMenuItem.Click += new System.EventHandler(this.Do_Add_Filter);
            // 
            // eliminarFiltroToolStripMenuItem
            // 
            this.eliminarFiltroToolStripMenuItem.Name = "eliminarFiltroToolStripMenuItem";
            this.eliminarFiltroToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.eliminarFiltroToolStripMenuItem.Text = "Eliminar Filtro";
            this.eliminarFiltroToolStripMenuItem.Click += new System.EventHandler(this.Do_Remove_Filter);
            // 
            // headerDataGridViewTextBoxColumn
            // 
            this.headerDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.headerDataGridViewTextBoxColumn.DataPropertyName = "Header";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Silver;
            this.headerDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            this.headerDataGridViewTextBoxColumn.HeaderText = "Nombre";
            this.headerDataGridViewTextBoxColumn.Name = "headerDataGridViewTextBoxColumn";
            this.headerDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // kColFilter
            // 
            this.kColFilter.DataPropertyName = "Type";
            this.kColFilter.DataSource = this.TypeBS;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kColFilter.DefaultCellStyle = dataGridViewCellStyle2;
            this.kColFilter.DisplayMember = "Text";
            this.kColFilter.HeaderText = "Filtro";
            this.kColFilter.Name = "kColFilter";
            this.kColFilter.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.kColFilter.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.kColFilter.ValueMember = "Value";
            // 
            // kColValue1
            // 
            this.kColValue1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kColValue1.DataPropertyName = "Value1";
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kColValue1.DefaultCellStyle = dataGridViewCellStyle3;
            this.kColValue1.HeaderText = "";
            this.kColValue1.Name = "kColValue1";
            this.kColValue1.ReadOnly = true;
            // 
            // kColValue2
            // 
            this.kColValue2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.kColValue2.DataPropertyName = "Value2";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.kColValue2.DefaultCellStyle = dataGridViewCellStyle4;
            this.kColValue2.HeaderText = "";
            this.kColValue2.Name = "kColValue2";
            this.kColValue2.ReadOnly = true;
            // 
            // GridSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ContextMenuStrip = this.cmsMenu;
            this.Controls.Add(this.grdRows);
            this.Name = "GridSelect";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(544, 260);
            ((System.ComponentModel.ISupportInitialize)(this.grdRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TypeBS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SelectRowBS)).EndInit();
            this.cmsMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grdRows;
        private System.Windows.Forms.BindingSource SelectRowBS;
        private System.Windows.Forms.BindingSource TypeBS;
        private System.Windows.Forms.ContextMenuStrip cmsMenu;
        private System.Windows.Forms.ToolStripMenuItem añadirFiltroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eliminarFiltroToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn headerDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewComboBoxColumn kColFilter;
        private System.Windows.Forms.DataGridViewTextBoxColumn kColValue1;
        private System.Windows.Forms.DataGridViewTextBoxColumn kColValue2;
    }
}
