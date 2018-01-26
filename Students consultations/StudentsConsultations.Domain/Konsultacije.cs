using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Konsultacije
    {
        public string Vreme { get; set; }

        [Key]
        public int NastavnikId { get; set; }

        [Key]
        public int StudentId { get; set; }

        [Key]
        public DateTime DatumKonsultacija { get; set; }

        public int RazlogId { get; set; }

        [ForeignKey("RazlogId")]
        [InverseProperty("Konsultacije")]
        public Razlog Razlog { get; set; }

        [ForeignKey("NastavnikId")]
        [InverseProperty("Konsultacije")]
        public Nastavnik Nastavnik { get; set; }

        [ForeignKey("DatumKonsultacija")]
        [InverseProperty("Konsultacije")]
        public Datum Datum { get; set; }

        [ForeignKey("StudentId")]
        [InverseProperty("Konsultacije")]
        public Student Student { get; set; }
    }
}
