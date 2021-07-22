using Net4Sage.Controls.DropDown;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Net4Sage.Controls.Lookup
{
    public partial class LookupForm : Form
    {
        private IEnumerable<object> Data;
        private List<Tuple<int, int, Dictionary<Int16, string>>> StaticColumns;
        public event LookupReturnEventHandler OnLookupReturn;
        public object SelectedData { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int DataCount { get => Data.Count(); }
        public bool HaveData { get => Data.Any(); }

        public LookupForm(IEnumerable<Object> data)
        {
            InitializeComponent();
            Data = data;
            StaticColumns = new List<Tuple<int, int, Dictionary<short, string>>>();
            
        }

        private void UpdateData(IEnumerable<Object> data)
        {
            GridNav.SetData(data);
        }

        //private void UpdateBinding<T>(IEnumerable<object> data) where T : class
        //{
        //    if(Data.Count > 0)
        //    {
        //        List<T> answer = new List<T>();
        //        foreach (T i in data)
        //            answer.Add(i);
        //        dataBS.DataSource = new Helpers.SortableBindingList<T>(answer);
        //    }
        //}

        private void Form_Load(object sender, EventArgs e)
        {
            BuildGrid();
        }

        private void BuildGrid()
        {
            bool autoShow = true;
            object model;
            Type dataType;
            CustomAttributeData attr;
            string name;
            int width;

            grdData.Columns.Clear();
            grdFilters.ClearSelects();

            List<string> properties = new List<string>();
            if ((model = Data.FirstOrDefault()) != null)
            {
                dataType = model.GetType();
                if ((attr = dataType.CustomAttributes.Where(p => p.AttributeType == typeof(LookupAutoShowColumns)).FirstOrDefault()) != null)
                    autoShow = (bool)attr.ConstructorArguments.First().Value;
                if ((attr = dataType.CustomAttributes.Where(p => p.AttributeType == typeof(LookupFormTitle)).FirstOrDefault()) != null)
                    Text = attr.ConstructorArguments.First().Value.ToString();
                foreach (PropertyInfo prop in dataType.GetProperties())
                {
                    if ((autoShow && prop.CustomAttributes.Where(p => p.AttributeType == typeof(LookupHideColumn)).Count() == 0) || (!autoShow && prop.CustomAttributes.Where(p => p.AttributeType == typeof(LookupShowColumn)).Count() != 0))
                    {
                        if ((attr = prop.CustomAttributes.Where(p => p.AttributeType == typeof(LookupColumnHeader)).FirstOrDefault()) != null)
                            name = attr.ConstructorArguments.First().Value.ToString();
                        else
                            name = prop.Name;
                        properties.Add(prop.Name);
                        if ((attr = prop.CustomAttributes.Where(p => p.AttributeType == typeof(LookupColumnHeaderWidth)).FirstOrDefault()) != null)
                            width = int.Parse(attr.ConstructorArguments.First().Value.ToString());
                        else
                            width = 100;

                        if ((attr = prop.CustomAttributes.Where(p => p.AttributeType == typeof(LookupStaticColumn)).FirstOrDefault()) != null)
                        {
                            grdData.Columns.Add(new DataGridViewTextBoxColumn()
                            {
                                Name = "kcol" + prop.Name,
                                HeaderText = name,
                                Visible = false,
                                DataPropertyName = prop.Name,
                                ReadOnly = true,
                            });
                            grdData.Columns.Add(new DataGridViewTextBoxColumn()
                            {
                                Name = "kcolStatic" + prop.Name,
                                HeaderText = name,
                                ReadOnly = true,
                                Width = width
                            });
                            StaticDropDown d = new StaticDropDown();
                            d.TableName = attr.ConstructorArguments[0].Value.ToString();
                            d.FieldName = attr.ConstructorArguments[1].Value.ToString();
                            d.SysSession = this.SysSession;
                            d.InitializeDropDown();
                            StaticColumns.Add(new Tuple<int, int, Dictionary<short, string>>(grdData.ColumnCount - 2, grdData.ColumnCount - 1, new Dictionary<short, string>()));
                            foreach (var i in d.GetValues())
                                StaticColumns[StaticColumns.Count - 1].Item3.Add(i.Value, i.Text);
                        }
                        else
                        {
                            grdData.Columns.Add(new DataGridViewTextBoxColumn()
                            {
                                Name = "kcol" + prop.Name,
                                HeaderText = name,
                                DataPropertyName = prop.Name,
                                ReadOnly = true,
                                Width = width,
                            });
                            if ((attr = prop.CustomAttributes.Where(p => p.AttributeType == typeof(LookupColumnHeaderSort)).FirstOrDefault()) != null)
                                grdData.Sort(grdData.Columns[grdData.ColumnCount - 1], (ListSortDirection)int.Parse(attr.ConstructorArguments[0].Value.ToString()));
                        }

                        if ((attr = prop.CustomAttributes.Where(p => p.AttributeType == typeof(LookupColumnFilter)).FirstOrDefault()) != null)
                            grdFilters.AddSelectRow(new GridSelect.SelectRow(attr.ConstructorArguments.First().Value.ToString(), prop.Name));

                    }
                }

                Do_Filtrate(null, null);
            }

            
        }

        [Obsolete]
        private void Do_Filtrate(object sender, EventArgs e)
        {
            IEnumerable<object> data = Data;
            grdFilters.FilterData(ref data);

            UpdateData(data);
        }

        private void Do_Select()
        {
            if (dataBS.Current != null)
            {
                SelectedData = dataBS.Current;
            }
            Hide();
            if (OnLookupReturn != null)
                OnLookupReturn.Invoke(this, new LookupReturnEventArgs()
                {
                    ReturnValue = SelectedData
                });
        }

        private void Do_Finish(object sender, EventArgs e)
        {
            Do_Select();
        }

        private void Do_Cancel(object sender, EventArgs e)
        {
            Hide();
            if (OnLookupReturn != null)
                OnLookupReturn.Invoke(this, new LookupReturnEventArgs()
                {
                    ReturnValue = null
                });
        }

        private void Do_Click_Select(object sender, MouseEventArgs e)
        {
            if (dataBS.Current != null) Do_Select();
        }

        private void On_DataRow_Added(object sender, DataGridViewRowsAddedEventArgs e)
        {
            string text;
            foreach(var i in this.StaticColumns)
            {
                try
                {
                    text = i.Item3[Int16.Parse(grdData.Rows[e.RowIndex].Cells[i.Item1].Value.ToString())];
                    grdData.Rows[e.RowIndex].Cells[i.Item2].Value = text;
                }
                catch
                {

                }
            }
        }
        /// <summary>
        /// Get an object from the lookup based on the Text Identifier
        /// </summary>
        /// <param name="textID">Text Identifier</param>
        /// <returns>object</returns>
        public object GetObjectByID(string textID)
        {
            PropertyInfo property;
            object temp;
            object answer = null;
            if (Data != null && Data.Any())
            {
                temp = Data.First();
                if ((property = temp.GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault()) != null)
                    answer = Data.Where(p =>
                    {
                        return property.GetValue(p).ToString() == textID;
                    }).FirstOrDefault();
            }
            return answer;
        }
        internal bool MoveTo(int index)
        {
            if (index < 0 || index >= DataCount)
                return false;
            try
            {
                OnLookupReturn?.Invoke(this, new LookupReturnEventArgs()
                {
                    ReturnValue = Data.ToArray()[index]
                });
            }
            catch(Exception exc)
            {
                if (SysSession != null)
                    SysSession.WriteLog(exc.Message);
                return false;
            }
            return true;
        }
        internal int IndexOf(object obj)
        {
            if(Data != null)
            {
                int n = 0;
                foreach (var i in Data)
                {
                    if (i.Equals(obj))
                        return n;
                    n++;
                }
            }
            return -1;
        }
    }
}
