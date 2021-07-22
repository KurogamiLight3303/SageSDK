using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Controls.Lookup
{
    [AttributeUsage(AttributeTargets.Class)]
    public class LookupAutoShowColumns : Attribute
    {
        public LookupAutoShowColumns(bool autoShow)
        {
        }
    }

    public class LookupFormTitle : Attribute
    {
        public LookupFormTitle(string Text)
        {

        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LookupShowColumn : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LookupHideColumn : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LookupColumnHeader : Attribute
    {
        public LookupColumnHeader(string Header)
        {

        }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LookupColumnFilter : Attribute
    {
        public LookupColumnFilter(string Header)
        {

        }
    }

    public class LookupTextReturn : Attribute
    {

    }

    public class LookupKeyReturn : Attribute
    {

    }

    [AttributeUsage(AttributeTargets.Property)]
    public class LookupStaticColumn : Attribute
    {
        public LookupStaticColumn(string tableName, string FieldName)
        {

        }
    }

    public class LookupColumnHeaderWidth : Attribute
    {
        public LookupColumnHeaderWidth(int width)
        {

        }
    }

    public class LookupColumnHeaderSort : Attribute
    {
        public LookupColumnHeaderSort(ListSortDirection direction = ListSortDirection.Ascending)
        {

        }
    }
}
