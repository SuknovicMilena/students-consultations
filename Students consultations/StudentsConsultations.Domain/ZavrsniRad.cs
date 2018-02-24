using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace StudentsConsultations.Data.Domain
{
    public class ZavrsniRad
    {
        [Key]
        [ForeignKey("Razlog")]
        public int RazlogId { get; set; }

        [Required]
        public string Tip { get; set; }

        public string NazivZavrsnogRada { get; set; }

        public Razlog Razlog { get; set; }
    }
}
