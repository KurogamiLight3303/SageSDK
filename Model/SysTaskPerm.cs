using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Model
{
    /// <summary>
    /// Perms of the Task
    /// </summary>
    public class SysTaskPerm
    {
        /// <summary>
        /// The UserGroupID
        /// </summary>
        public string UserGroupID { get; set; }
        /// <summary>
        /// ID of the Task
        /// </summary>
        public int TaskID { get; set; }
        /// <summary>
        /// Level of Security
        /// </summary>
        [Column("Rights")]
        public TaskPermision SecurityLvl { get; set; }
    }
}
