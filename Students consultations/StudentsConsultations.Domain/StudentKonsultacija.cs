using StudentsConsultations.Data.Domain;
using System;

namespace StudentsConsultations.Data.Domain
{
    public class StudentKonsultacija
    {
        public int Id { get; set; }

        public bool Odrzane { get; set; }

        public DateTime VremeOd { get; set; }

        public DateTime VremeDo { get; set; }

        public int NastavnikId { get; set; }

        public int StudentId { get; set; }

        public int KonsultacijaId { get; set; }

        public int RazlogId { get; set; }

        public Konsultacija Konsultacija { get; set; }

        public Nastavnik Nastavnik { get; set; }

        public Razlog Razlog { get; set; }

        public Student Student { get; set; }
    }
}
