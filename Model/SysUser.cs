using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Net4Sage.Model
{
    public class SysUser
    {
        public string UserID { get; set; }
        [Column("UserBusinessDate")]
        public DateTime? BusinessDate { get; set; }
        [Column("SysDateAsBusDate")]
        public bool UseSystemDate { get; set; }
    }
}
