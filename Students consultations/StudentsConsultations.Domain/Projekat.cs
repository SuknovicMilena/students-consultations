using StudentsConsultations.Data.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsConsultations.Data.Domain
{
    public class Projekat
    {
        [Key]
        [ForeignKey("Razlog")]
        public int RazlogId { get; set; }

        [Required]
        public string NazivProjekta { get; set; }

        [Required]
        public string NazivIspita { get; set; }

        public Razlog Razlog { get; set; }
    }
}
