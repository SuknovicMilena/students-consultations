using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Student : Korisnik
    {
        public string BrojIndeksa { get; set; }

        [InverseProperty("Student")]
        public List<Zadatak> Zadaci { get; set; }

        [InverseProperty("Student")]
        public ICollection<Konsultacije> Konsultacije { get; set; } = new HashSet<Konsultacije>();
    }
}
