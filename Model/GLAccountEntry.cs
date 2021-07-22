using Net4Sage.Controls.Lookup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net4Sage.Model
{
    /// <summary>
    /// Accounts Entry
    /// </summary>
    [LookupAutoShowColumns(false)]
    [LookupFormTitle("Buscar Cuenta de GL")]
    public class GLAccountEntry
    {
        /// <summary>
        /// Account Key
        /// </summary>
        [Column("GLAcctKey")]
        [LookupKeyReturn]
        public int AccountKey { get; set; }
        /// <summary>
        /// The number of the account
        /// </summary>
        [Column("GLAcctNo")]
        [LookupTextReturn]
        [LookupShowColumn]
        [LookupColumnFilter("No de Cuenta")]
        [LookupColumnHeader("No de Cuenta")]
        public string AccountNumber { get; set; }
        /// <summary>
        /// Description of the Account
        /// </summary>
        [LookupColumnFilter("Descripción")]
        [LookupColumnHeader("Descripción")]
        [LookupShowColumn]
        public string Description { get; set; }
        /// <summary>
        /// Category of the Account
        /// </summary>
        [Column("AcctCatID")]
        public string Category { get; set; }
        /// <summary>
        /// Description from the Category of the Account
        /// </summary>
        [Column("AcctCatDesc")]
        [LookupColumnHeader("Categoría")]
        [LookupShowColumn]
        public string CategoryDescription { get; set; }
        /// <summary>
        /// Account Type
        /// </summary>
        [Column("AcctTypeID")]
        public string Type { get; set; }
        /// <summary>
        /// Account Type Description
        /// </summary>
        [Column("AcctTypeDesc")]
        [LookupColumnHeader("Tipo")]
        [LookupShowColumn]
        public string TypeDescription { get; set; }
        /// <summary>
        /// Status of the Account
        /// </summary>
        [Column("StatusAsText")]
        [LookupColumnHeader("Estado")]
        [LookupShowColumn]
        public string Status { get; set; }
    }
}
