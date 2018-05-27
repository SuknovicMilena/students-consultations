using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Data.Domain
{
    public class Konsultacija
    {
        public int Id { get; set; }

        public int DanUNedelji { get; set; }

        public DateTime VremeOd { get; set; }

        public DateTime VremeDo { get; set; }

        public int NastavnikId { get; set; }

        [ForeignKey("NastavnikId")]
        [InverseProperty("Konsultacija")]
        public Nastavnik Nastavnik { get; set; }

        public ICollection<StudentKonsultacija> StudentKonsultacija { get; set; } = new HashSet<StudentKonsultacija>();
    }
}
