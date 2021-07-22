using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage
{
    /// <summary>
    /// Companies
    /// </summary>
    public class CompanyPairsValues
    {
        /// <summary>
        /// ID of the company
        /// </summary>
        public string CompanyID { get; set; }
        /// <summary>
        /// Name of the company
        /// </summary>
        public string CompanyName { get; set; }
        /// <summary>
        /// Get Company full text
        /// </summary>
        /// <returns>Company full text</returns>
        public override string ToString()
        {
            return CompanyID + " - " + CompanyName;
        }
    }
}
