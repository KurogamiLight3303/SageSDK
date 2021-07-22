using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using Net4Sage.Controls.Lookup;

namespace Net4Sage.Controls.GridSelect
{
    /// <summary>
    /// Selection Row Entry
    /// </summary>
    public class SelectRow : ICloneable
    {
        internal bool Handled { get; set; }
        /// <summary>
        /// Current Type of Filtrer
        /// </summary>
        public SelectRowTypes Type { get; set; }
        /// <summary>
        /// Header Display on the Control
        /// </summary>
        public string Header { get; private set; }
        /// <summary>
        /// Current String Value 1
        /// </summary>
        public string Value1 { get; set; }
        /// <summary>
        /// Current String Value 2
        /// </summary>
        public string Value2 { get; set; }
        /// <summary>
        /// The Current Datasource
        /// </summary>
        public object DataSource { get; private set; }
        /// <summary>
        /// Name of the Property
        /// </summary>
        public string PropertyName { get; private set; }
        /// <summary>
        /// Type of the Selection Row Entry
        /// </summary>
        public SelectRowDataType DataType { get; set; }
        /// <summary>
        /// Create instance of the Selection Row
        /// </summary>
        /// <param name="header">Header</param>
        /// <param name="propertyName">Property Name</param>
        /// <param name="dataType">Selection Row Entry Type</param>
        public SelectRow(string header, string propertyName, SelectRowDataType dataType = SelectRowDataType.Text) : this(header, propertyName, dataType, null)
        {
        }
        [Obsolete]
        public SelectRow(string header, string propertyName, Type type) : this(header, propertyName, type, null)
        {

        }
        [Obsolete]
        public SelectRow(string header, string propertyName, Type type, object datasource)
        {
            Value1 = Value2 = "";
            Type = SelectRowTypes.All;
            Header = header;
            PropertyName = propertyName;
            if (typeof(IEnumerable<object>) == type)
                DataType = SelectRowDataType.Lookup;
            else if (typeof(IEnumerable<string>) == type)
                DataType = SelectRowDataType.TextLookup;
            else if (typeof(DateTime) == type)
                DataType = SelectRowDataType.Date;
            else
                DataType = SelectRowDataType.Text;
            DataSource = datasource;
        }
        public SelectRow(string header, string propertyName, SelectRowDataType type, object datasource)
        {
            Value1 = Value2 = "";
            Type = SelectRowTypes.All;
            Header = header;
            PropertyName = propertyName;
            DataType = type;
            DataSource = datasource;
        }
        public object Clone()
        {
            return new SelectRow(Header, PropertyName, DataType, DataSource);
        }
        public string GetSQLFilter()
        {
            string answer = string.Empty;
            switch (Type)
            {
                case SelectRowTypes.All:
                    answer = "1=1";
                    break;
                case SelectRowTypes.Equal:
                    answer = PropertyName + " = " + (DataType != SelectRowDataType.Lookup ? SageSession.QuoteString((DataType == SelectRowDataType.Date ? DateTime.Parse(Value1.ToString()).ToString("yyyy-MM-dd") : Value1.ToString())) : GetFromDataSourceKey(Value1.ToString()));
                    break;
                case SelectRowTypes.NotEqual:
                    answer = PropertyName + " <> " + (DataType != SelectRowDataType.Lookup ? SageSession.QuoteString((DataType == SelectRowDataType.Date ? DateTime.Parse(Value1.ToString()).ToString("yyyy-MM-dd") : Value1.ToString())) : GetFromDataSourceKey(Value1.ToString()));
                    break;
                case SelectRowTypes.Between:
                    if (DataType == SelectRowDataType.Date)
                        answer = PropertyName + " between " + SageSession.QuoteString(DateTime.Parse(Value1.ToString()).ToString("yyyy-MM-dd")) + " and " + SageSession.QuoteString(DateTime.Parse(Value2.ToString()).ToString("yyyy-MM-dd"));
                    break;
                case SelectRowTypes.StartWith:
                    if (DataType == SelectRowDataType.TextLookup || DataType == SelectRowDataType.Text)
                        answer = PropertyName + " like " + SageSession.QuoteString(Value1.ToString() + "%");
                    break;
                case SelectRowTypes.EndWith:
                    if (DataType == SelectRowDataType.TextLookup || DataType == SelectRowDataType.Text)
                        answer = PropertyName + " like " + SageSession.QuoteString("%" + Value1.ToString());
                    break;
            }
            return answer;
        }

        internal string GetFromDataSourceKey(string value)
        {
            
            PropertyInfo tempProp = ((IEnumerable<object>)DataSource).FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupTextReturn)).Count() != 0).FirstOrDefault();
            object tempData = ((IEnumerable<object>)DataSource).Where(p => tempProp.GetValue(p, null).ToString().ToLower() == value.ToLower()).FirstOrDefault();
            tempProp = ((IEnumerable<object>)DataSource).FirstOrDefault().GetType().GetProperties().Where(p => p.CustomAttributes.Where(s => s.AttributeType == typeof(LookupKeyReturn)).Count() != 0).FirstOrDefault();
            object val = tempData != null ? tempProp.GetValue(tempData) : -1;
            if (val.GetType() == typeof(string))
                return SageSession.QuoteString(val.ToString());
            else
                return val.ToString();
        }
    }
}