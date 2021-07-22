using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Model
{
    /// <summary>
    /// Data of the company
    /// </summary>
    public class SysCompany
    {
        /// <summary>
        /// CompanyID
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// Home Currency ID
        /// </summary>
        [Column("CurrID")]
        public string HomeCurrID { get; set; }
        //Home Country ID
        [Column("CountryID")]
        public string HomeCountryID { get; set; }
    }
}
