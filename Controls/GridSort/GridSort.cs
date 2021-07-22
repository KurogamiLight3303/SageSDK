using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using CrystalDecisions.CrystalReports.Engine;

namespace Net4Sage.Controls.GridSort
{
    public partial class GridSort : UserControl
    {
        private List<Tuple<string, string>> data = new List<Tuple<string, string>>();
        private int _maxGroups;
        public int MaxGroups { get { return _maxGroups; } set { if (value > 0) _maxGroups = value; else _maxGroups = 1; } }
        public GridSort()
        {
            InitializeComponent();
            MaxGroups = 1;
        }

        public void SetData(Type ReportObject)
        {
            CustomAttributeData attr;
            SortRowsBS.Clear();
            data.Clear();
            foreach (var p in ReportObject.GetProperties())
                if ((attr = p.CustomAttributes.Where(s => s.AttributeType == typeof(SortSelectField)).FirstOrDefault()) != null)
                    data.Add(new Tuple<string, string>(p.Name, attr.ConstructorArguments[0].ToString().Replace("\"", "")));
            ((DataGridViewComboBoxColumn)grdRows.Columns[0]).Items.Clear();
            foreach (var s in data)
                ((DataGridViewComboBoxColumn)grdRows.Columns[0]).Items.Add(s.Item2);
        }

        public void FormatReport(ReportClass report)
        {
            Section section;
            PropertyInfo property;
            int count = 1;
            grdRows.EndEdit();
            foreach(SortRow r in SortRowsBS.List)
            {
                if (r.GroupBy)
                {
                    report.DataDefinition.FormulaFields["GroupByDynamic"+count].Text = "{" + report.Database.Tables[0].Name + "."+ data.Where(p => p.Item2 == r.Field).FirstOrDefault().Item1 + "}";
                    report.DataDefinition.FormulaFields["GroupNameDynamic" + count].Text = "\"" + r.Field + ": \" & {" + report.Database.Tables[0].Name + "." + data.Where(p => p.Item2 == r.Field).FirstOrDefault().Item1 + "}";
                    if ((property = report.GetType().GetProperties().Where(s => s.Name == "GroupHeaderSection" + count).FirstOrDefault()) != null)
                        if ((section = property.GetValue(report) as Section) != null)
                            section.SectionFormat.EnableSuppress = false;
                    if ((property = report.GetType().GetProperties().Where(s => s.Name == "GroupFooterSection" + count).FirstOrDefault()) != null)
                        if ((section = property.GetValue(report) as Section) != null)
                            section.SectionFormat.EnableSuppress = !r.Subtotalize;
                    count++;
                }
            }
            
        }

        private void On_Row_Added(object sender, DataGridViewRowsAddedEventArgs e)
        {
            CheckIfRowAvaliable();
        }

        private void On_RowCount_Change(object sender, DataGridViewRowEventArgs e)
        {
            CheckIfRowAvaliable();
        }

        private void CheckIfRowAvaliable()
        {
            if (grdRows.Rows != null && grdRows.Rows.Count > MaxGroups)
            {
                grdRows.AllowUserToAddRows = false;
            }
            else if (!grdRows.AllowUserToAddRows)
            {
                grdRows.AllowUserToAddRows = true;
            }
        }

        private void OnCellValidating(object sender,DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex != 0) return;
            string value = e.FormattedValue.ToString();
            for(var i = 0; i < grdRows.RowCount; i++)
            {
                if (i != e.RowIndex && grdRows[0, i].Value != null && grdRows[0, i].Value.ToString() == value)
                {
                    ((DataGridViewComboBoxCell)grdRows.Rows[e.RowIndex].Cells[0]).Value = null;
                    return;
                }
            }
        }
    }
}
