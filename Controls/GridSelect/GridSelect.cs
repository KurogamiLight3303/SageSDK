using Net4Sage.Controls.Lookup;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Net4Sage.Controls.GridSelect
{
    /// <summary>
    /// Control for Data Filter
    /// </summary>
    public partial class GridSelect : UserControl
    {
        private List<SelectRowTypeEntry> Types;
        /// <summary>
        /// Create Instance of Control
        /// </summary>
        public GridSelect()
        {
            InitializeComponent();
            Types = new List<SelectRowTypeEntry>();
            Types.Add(new SelectRowTypeEntry()
            {
                Text = "Todos",
                Value = SelectRowTypes.All
            });
            Types.Add(new SelectRowTypeEntry()
            {
                Text = "Igual a",
                Value = SelectRowTypes.Equal
            });
            Types.Add(new SelectRowTypeEntry()
            {
                Text = "Diferente de",
                Value = SelectRowTypes.NotEqual
            });

            Types.Add(new SelectRowTypeEntry()
            {
                Text = "Comienza por",
                Value = SelectRowTypes.StartWith
            });

            Types.Add(new SelectRowTypeEntry()
            {
                Text = "Termina en",
                Value = SelectRowTypes.EndWith
            });

            Types.Add(new SelectRowTypeEntry()
            {
                Text = "Entre",
                Value = SelectRowTypes.Between
            });

            foreach (var i in Types)
                TypeBS.Add(i);
        }

        internal void ClearSelects()
        {
            SelectRowBS.Clear();
        }

        #region "Data Filter"
        /// <summary>
        /// Add Filter Row
        /// </summary>
        /// <param name="row"></param>
        public void AddSelectRow(SelectRow row)
        {
            SelectRowBS.Add(row);
        }
        /// <summary>
        /// Get the list of the active filters used in the component
        /// </summary>
        /// <returns>List of Filters</returns>
        public List<SelectRow> GetFilterUsed()
        {
            grdRows.EndEdit();
            List<SelectRow> answer = new List<SelectRow>();
            foreach (DataGridViewRow r in grdRows.Rows)
            {
                if (((SelectRowTypes)((DataGridViewComboBoxCell)r.Cells["kColFilter"]).Value) != SelectRowTypes.All)
                {
                    answer.Add((SelectRow)r.DataBoundItem);
                }
            }

            return answer;
        }
        /// <summary>
        /// Filter Data
        /// </summary>
        /// <param name="data">Collection of Data</param>
        [Obsolete]
        public void FilterData(ref IEnumerable<object> data)
        {
            FilterData<object>(ref data);
        }
        /// <summary>
        /// Filter Data
        /// </summary>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <param name="data">Collection of Data</param>
        public void FilterData<T>(ref IEnumerable<T> data) where T : class
        {
            foreach (var f in GetFilterUsed().GroupBy(p => p.PropertyName))
            {
                if (f.Count() > 1)
                {
                    IEnumerable<T> source = data.ToList();
                    IEnumerable<T> temp, answer = new List<T>();

                    foreach (var f2 in f)
                    {
                        temp = source.ToList();
                        UseFilter(ref temp, f2);
                        answer = answer.Union(temp).ToList();
                    }
                    data = answer;
                }
                else
                    UseFilter(ref data, f.FirstOrDefault());
            }
        }
        /// <summary>
        /// User Filter on Data Collection
        /// </summary>
        /// <param name="data">Collection of Data</param>
        /// <param name="filter">Filter to use</param>
        [Obsolete]
        public void UseFilter(ref IEnumerable<object> data, SelectRow filter)
        {
            UseFilter<object>(ref data, filter);
        }
        /// <summary>
        /// User Filter on Data Collection
        /// </summary>
        /// <typeparam name="T">Type of the data</typeparam>
        /// <param name="data">Collection of Data</param>
        /// <param name="filter">Filter to use</param>
        public void UseFilter<T>(ref IEnumerable<T> data, SelectRow filter) where T : class
        {
            PropertyInfo property, tempProp;
            object model, tempData;
            string key;
            try
            {
                if ((model = data.FirstOrDefault()) != null && filter.Value1 != null)
                {
                    property = model.GetType().GetProperty(filter.PropertyName);
                    switch (filter.Type)
                    {
                        case SelectRowTypes.Equal:
                            if (filter.DataType == SelectRowDataType.Lookup)
                            {
                                tempProp = ((IEnumerable<object>)filter.DataSource).FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault();
                                tempData = ((IEnumerable<object>)filter.DataSource).Where(p => tempProp.GetValue(p, null).ToString() == filter.Value1.ToString()).FirstOrDefault();
                                tempProp = ((IEnumerable<object>)filter.DataSource).FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupKeyReturn)).Count() != 0).FirstOrDefault();
                                key = tempProp.GetValue(tempData).ToString();
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString() == key.ToString()));
                            }
                            else if (filter.DataType == SelectRowDataType.TextLookup)
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString().ToLower() == filter.Value1.ToString().ToLower()));
                            else if (filter.DataType == SelectRowDataType.Date)
                            {
                                DateTime temp = DateTime.Parse(filter.Value1.ToString(), CultureInfo.CurrentUICulture).Date;
                                data = new List<T>(data.Where(p => property.GetValue(p, null).Equals(temp)));
                            }
                            else
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString().ToLower() == filter.Value1.ToString().ToLower()));
                            break;
                        case SelectRowTypes.NotEqual:
                            if (filter.DataType == SelectRowDataType.Lookup)
                            {
                                tempProp = ((IEnumerable<object>)filter.DataSource).FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault();
                                tempData = ((IEnumerable<object>)filter.DataSource).Where(p => tempProp.GetValue(p, null).ToString() == filter.Value1.ToString()).FirstOrDefault();
                                tempProp = ((IEnumerable<object>)filter.DataSource).FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupKeyReturn)).Count() != 0).FirstOrDefault();
                                key = tempProp.GetValue(tempData).ToString();
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString() != key));
                            }
                            else if (filter.DataType == SelectRowDataType.TextLookup)
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString().ToLower() != filter.Value1.ToString().ToLower()));
                            else
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString() != filter.Value1));
                            break;
                        case SelectRowTypes.StartWith:
                            if (filter.DataType == SelectRowDataType.Date)
                                data = new List<T>(data.Where(p => DateTime.Parse(property.GetValue(p, null).ToString(), CultureInfo.CurrentUICulture) >= DateTime.Parse((filter.Value1), CultureInfo.CurrentUICulture)));
                            else
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString().ToLower().StartsWith(filter.Value1.ToLower())));
                            break;
                        case SelectRowTypes.EndWith:
                            if (filter.DataType == SelectRowDataType.Date)
                                data = new List<T>(data.Where(p => DateTime.Parse(property.GetValue(p, null).ToString(), CultureInfo.CurrentUICulture) <= DateTime.Parse((filter.Value1), CultureInfo.CurrentUICulture)));
                            else
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString().ToLower().EndsWith(filter.Value1.ToLower())));
                            break;
                        case SelectRowTypes.Between:
                            if (filter.DataType == SelectRowDataType.Date && property.PropertyType == typeof(DateTime))
                            {
                                DateTime temp1, temp2;
                                try
                                {
                                    temp1 = DateTime.Parse(filter.Value1.ToString(), CultureInfo.CurrentUICulture).Date;
                                    temp2 = DateTime.Parse(filter.Value1.ToString(), CultureInfo.CurrentUICulture).Date;
                                }catch(Exception)
                                {
                                    try
                                    {
                                        temp1 = DateTime.Parse(filter.Value1.ToString(), CultureInfo.CurrentCulture).Date;
                                        temp2 = DateTime.Parse(filter.Value1.ToString(), CultureInfo.CurrentCulture).Date;
                                    }
                                    catch (Exception)
                                    {
                                        return;
                                    }
                                }
                                data = new List<T>(data.Where(p => (DateTime)property.GetValue(p, null) >= temp1 && (DateTime)property.GetValue(p, null) <= temp2));
                            }   
                            else if (filter.DataType == SelectRowDataType.TextLookup)
                            {
                                tempProp = ((IEnumerable<object>)filter.DataSource).FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault();
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString().CompareTo(filter.Value1) >= 0 && property.GetValue(p, null).ToString().CompareTo(filter.Value2) <= 0));
                            }
                            else
                                data = new List<T>(data.Where(p => property.GetValue(p, null).ToString().CompareTo(filter.Value1) >= 0 && property.GetValue(p, null).ToString().CompareTo(filter.Value2) <= 0));
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Ha ocurrido error: " + e.Message, "Sage MAS 500", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// Get if there is are all Filters Used
        /// </summary>
        /// <returns>Result</returns>
        public bool AreAllFiltersUsed()
        {
            grdRows.EndEdit();
            foreach (SelectRow i in SelectRowBS.List)
            {
                if (!i.Handled && i.Type == SelectRowTypes.All)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Get number of Filters used
        /// </summary>
        /// <returns>Number of Filters used</returns>
        public int FiltersUsedCount()
        {
            int answer = 0;
            foreach (SelectRow i in SelectRowBS.List)
                if (i.Type != SelectRowTypes.All)
                    answer++;
            return answer;
        }
        /// <summary>
        /// Get if there is any filter used
        /// </summary>
        /// <returns></returns>
        public bool AreFiltersUsed()
        {
            grdRows.EndEdit();
            foreach (SelectRow i in SelectRowBS.List)
            {
                if (i.Type != SelectRowTypes.All)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// Clear the values and states of the filters
        /// </summary>
        public void ClearFilters()
        {
            grdRows.EndEdit();
            On_FilterType_Change(null, null);
            for (var i = 0; i < grdRows.RowCount; i++)
            {
                while (i < grdRows.RowCount && ((SelectRow)grdRows.Rows[i].DataBoundItem).Handled)
                    grdRows.Rows.RemoveAt(i);
                if (i < grdRows.RowCount)
                {
                    ((SelectRow)grdRows.Rows[i].DataBoundItem).Type = SelectRowTypes.All;
                    UpdateFilter(null, grdRows.Rows[i]);
                }
            }
        }
        /// <summary>
        /// Get SQL filters
        /// </summary>
        /// <returns>SQL Filters</returns>
        public string GetSQLFilters()
        {
            List<string> answer = new List<string>();
            foreach (var f in GetFilterUsed().GroupBy(p => p.PropertyName))
                if (f.Count() == 1)
                    answer.Add(f.FirstOrDefault().GetSQLFilter());
                else
                    answer.Add("(" +string.Join(" or ", f.Select(p => p.GetSQLFilter())) +")");

                return string.Join(" and ", answer);
        }
        #endregion

        #region "Control Events"
        private void On_Control_Showing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            DataGridViewComboBoxEditingControl cmb = e.Control as DataGridViewComboBoxEditingControl;
            if (cmb != null)
            {
                cmb.SelectionChangeCommitted -= new EventHandler(On_FilterType_Change);
                cmb.SelectionChangeCommitted += new EventHandler(On_FilterType_Change);
            }
        }
        private void On_FilterType_Change(object sender, EventArgs e)
        {
            UpdateFilter(sender, grdRows.CurrentRow);
        }
        private void UpdateFilter(object sender, DataGridViewRow row)
        {
            DataGridViewComboBoxCell cmb = row.Cells["kColFilter"] as DataGridViewComboBoxCell;
            SelectRowTypeEntry type;
            if (cmb.Value == null || cmb.ToString() == cmb.Value.ToString()) return;
            if ((type = Types.Where(p => p.Text == cmb.EditedFormattedValue.ToString()).FirstOrDefault()) != null)
            {
                switch (type.Value)
                {
                    case SelectRowTypes.All:
                        row.Cells["kColValue1"].ReadOnly = true;
                        //row.Cells["kColValue1"].Value = "";
                        ((SelectRow)SelectRowBS.Current).Value1 = "";
                        grdRows.UpdateCellValue(2, grdRows.CurrentCell.RowIndex);
                        row.Cells["kColValue2"].ReadOnly = true;
                        //row.Cells["kColValue2"].Value = "";
                        break;
                    case SelectRowTypes.Between:
                        row.Cells["kColValue1"].ReadOnly = false;

                        row.Cells["kColValue2"].ReadOnly = false;
                        break;
                    default:

                        if (((SelectRow)row.DataBoundItem).DataType == SelectRowDataType.Lookup && (type.Value == SelectRowTypes.StartWith || type.Value == SelectRowTypes.EndWith))
                            ((DataGridViewComboBoxEditingControl)sender).SelectedIndex = 1;
                        row.Cells["kColValue1"].ReadOnly = false;
                        //row.Cells["kColValue1"].Value = "";

                        row.Cells["kColValue2"].ReadOnly = true;
                        //row.Cells["kColValue2"].Value = "";
                        break;
                }

            }
        }
        private void On_Cell_Leaves(object sender, DataGridViewCellEventArgs e)
        {
        }
        private void Cell_Click(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1) return;
            object value1 = grdRows.Rows[e.RowIndex].Cells["kColValue1"].Value, value2 = grdRows.Rows[e.RowIndex].Cells["kColValue2"].Value;
            if (((SelectRow)grdRows.Rows[e.RowIndex].DataBoundItem).DataType == SelectRowDataType.Date)
            {
                grdRows.Columns["kColValue1"].CellTemplate = new DataGridViewCalendarCell();
                grdRows.Columns["kColValue2"].CellTemplate = new DataGridViewCalendarCell();
                var cell1 = new DataGridViewCalendarCell()
                {
                    Value = value1,
                };
                var cell2 = new DataGridViewCalendarCell()
                {
                    Value = value2,
                };
                grdRows.Rows[e.RowIndex].Cells["kColValue1"] = cell1;
                grdRows.Rows[e.RowIndex].Cells["kColValue2"] = cell2;
            }
            else if (((SelectRow)grdRows.Rows[e.RowIndex].DataBoundItem).DataType == SelectRowDataType.Lookup || ((SelectRow)grdRows.Rows[e.RowIndex].DataBoundItem).DataType == SelectRowDataType.TextLookup)
            {
                grdRows.Columns["kColValue1"].CellTemplate = new DataGridViewTextLookupCell();
                grdRows.Columns["kColValue2"].CellTemplate = new DataGridViewTextLookupCell();
                var cell1 = new DataGridViewTextLookupCell()
                {
                    Value = value1,
                    DataSource = ((SelectRow)grdRows.Rows[e.RowIndex].DataBoundItem).DataSource as IEnumerable<object>,
                };
                var cell2 = new DataGridViewTextLookupCell()
                {
                    Value = value2,
                    DataSource = ((SelectRow)grdRows.Rows[e.RowIndex].DataBoundItem).DataSource as IEnumerable<object>
                };
                grdRows.Rows[e.RowIndex].Cells["kColValue1"] = cell1;
                grdRows.Rows[e.RowIndex].Cells["kColValue2"] = cell2;
            }
            else
            {
                grdRows.Columns["kColValue1"].CellTemplate = new DataGridViewTextBoxCell();
                grdRows.Columns["kColValue2"].CellTemplate = new DataGridViewTextBoxCell();
                var cell1 = new DataGridViewTextBoxCell()
                {
                    Value = value1,
                };
                var cell2 = new DataGridViewTextBoxCell()
                {
                    Value = value2,
                };
                grdRows.Rows[e.RowIndex].Cells["kColValue1"] = cell1;
                grdRows.Rows[e.RowIndex].Cells["kColValue2"] = cell2;
            }
        }
        private void Do_Add_Filter(object sender, EventArgs e)
        {
            if (grdRows.CurrentRow != null)
            {
                SelectRow temp = ((SelectRow)grdRows.CurrentRow.DataBoundItem).Clone() as SelectRow;
                temp.Handled = true;
                SelectRowBS.Add(temp);
            }
        }
        private void Do_Remove_Filter(object sender, EventArgs e)
        {
            if (grdRows.CurrentRow != null && ((SelectRow)grdRows.CurrentRow.DataBoundItem).Handled)
                grdRows.Rows.Remove(grdRows.CurrentRow);
        }
        #endregion
    }
}
