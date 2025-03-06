using System.ComponentModel.DataAnnotations.Schema;

namespace Slovko.NL.Api.DataAccess
{
    [Table("FiveLetterWord")]
    public class FiveLetterWord
    {
        [Column("Id")]
        public int Id { get; set; }

        [Column("Word")]
        public string Word { get; set; }

        [Column("Entropy")]
        public double Entropy { get; set; }
    }
}
