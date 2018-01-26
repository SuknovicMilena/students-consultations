using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF
{
    public class Konsultacije
    {
        public int StudentId { get; set; }
        public int NastavnikId { get; set; }
        public DateTime DatumKonsultacija { get; set; }

        public int RazlogId { get; set; }

        public Datum DatumObjekat { get; set; }
        public Nastavnik Nastavnik { get; set; }
        public Razlog Razlog { get; set; }
        public Student Student { get; set; }
    }
}
