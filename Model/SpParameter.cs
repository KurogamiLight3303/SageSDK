using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Model
{
    public class SpParameter
    {
        public string Name { get; set; }
        public DbType ParameterType { get; set; }
        public ParameterDirection Direction { get; set; }
        public Object Value { get; set; }
        public SpParameter()
        {
            Direction = ParameterDirection.Input;
        }

    }
}
