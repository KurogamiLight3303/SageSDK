using System.ComponentModel.DataAnnotations.Schema;

namespace Net4Sage.Controls.DropDown
{
    public class StaticListEntry
    {
        [Column("DBValue")]
        public short Value { get; set; }
        [Column("LocalText")]
        public string Text { get; set; }
        public int StringNo { get; set; }
        public bool IsDefault { get; set; }
        public bool IsHidden { get; set; }
    }
}
