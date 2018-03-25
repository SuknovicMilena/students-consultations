using StudentsConsultations.Models.Nastavnik;
using StudentsConsultations.Models.Projekat;
using StudentsConsultations.Models.Razlog;
using StudentsConsultations.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Konsultacije
{
    public class KonsultacijeRowDto
    {
        public int StudentId { get; set; }

        public int NastavnikId { get; set; }

        public int RazlogId { get; set; }

        public bool Odrzane { get; set; }

        public DateTime DatumKonsultacija { get; set; }

        public RazlogDto Razlog { get; set; }

        public StudentDto Student { get; set; }

        public NastavnikDto Nastavnik { get; set; }
    }
}
