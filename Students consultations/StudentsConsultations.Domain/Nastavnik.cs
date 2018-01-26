using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Nastavnik : Korisnik
    {
        public int BrojRadneKnjizice { get; set; }
   
        [InverseProperty("Nastavnik")]
        public List<Zadatak> Zadaci { get; set; }

        [InverseProperty("Nastavnik")]
        public ICollection<Konsultacije> Konsultacije { get; set; } = new HashSet<Konsultacije>();
    }
}
