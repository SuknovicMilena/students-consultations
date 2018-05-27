using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsConsultations.Data.Domain
{
    public class Student 
    {
        public int Id { get; set; }

        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }

        [Required]
        public string BrojIndeksa { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }

        public ICollection<StudentKonsultacija> StudentKonsultacija { get; set; } = new HashSet<StudentKonsultacija>();
    }
}
