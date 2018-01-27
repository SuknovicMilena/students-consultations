using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsConsultations.Domain
{
    public class Razlog
    {
        [Key]
        public int RazlogId { get; set; }

        [Required]
        public string Opis { get; set; }

        public Ispit Ispit { get; set; }

        public ZavrsniRad ZavrsniRad { get; set; }

        public Projekat Projekat { get; set; }

        public ICollection<Konsultacije> Konsultacije { get; set; } = new HashSet<Konsultacije>();
    }
}
