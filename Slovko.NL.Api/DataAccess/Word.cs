using System.ComponentModel.DataAnnotations.Schema;

namespace Slovko.NL.Api.DataAccess
{
    [Table("fiveletterwords")]
    public class Word
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("value")]
        public string Value { get; set; }

        [Column("entropy")]
        public double Entropy { get; set; }
    }
}
