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
    public class StudentKonsultacijaRowDto
    {
        public int Id { get; set; }

        public int StudentId { get; set; }

        public int NastavnikId { get; set; }

        public int RazlogId { get; set; }

        public bool Odrzane { get; set; }

        public DateTime DatumKonsultacija { get; set; }

        public string DatumString { get; set; }

        public DateTime VremeOd { get; set; }

        public DateTime VremeDo { get; set; }

        public int KonsultacijaId { get; set; }

        public KonsultacijaDto Konsultacija { get; set; }

        public RazlogDto Razlog { get; set; }

        public StudentDto Student { get; set; }

        public NastavnikDto Nastavnik { get; set; }



    }
}
