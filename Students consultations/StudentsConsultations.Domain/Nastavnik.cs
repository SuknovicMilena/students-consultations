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

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }

        public ICollection<StudentKonsultacija> StudentKonsultacija { get; set; } = new HashSet<StudentKonsultacija>();

        public ICollection<Konsultacija> Konsultacija { get; set; } = new HashSet<Konsultacija>();
    }
}
