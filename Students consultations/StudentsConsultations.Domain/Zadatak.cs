using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsConsultations.Domain
{
    public class Zadatak
    {
        public int Id { get; set; }

        public int NastavnikId { get; set; }

        public int StudentId { get; set; }

        public DateTime DatumKonsultacija { get; set; }

        [Required]
        public int VrstaZadatkaId { get; set; }

        public string RokDoZavrsetka { get; set; }

        public string Opis { get; set; }

        public Nastavnik Nastavnik { get; set; }

        public Student Student { get; set; }

        public Datum Datum { get; set; }

        [ForeignKey("VrstaZadatkaId")]
        [InverseProperty("Zadaci")]
        public VrstaZadatka VrstaZadatka { get; set; }
    }
}
