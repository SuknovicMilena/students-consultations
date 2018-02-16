using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsConsultations.Data.Domain
{
    public class Nastavnik 
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public string BrojRadneKnjizice { get; set; }

        public ICollection<Konsultacije> Konsultacije { get; set; } = new HashSet<Konsultacije>();


    }
}
