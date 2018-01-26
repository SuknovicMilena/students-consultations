using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Zadatak : OpstiEntitet
    {
        [Required]
        public int NastavnikId { get; set; }

        [Required]
        public int StudentId { get; set; }

        [Required]
        public DateTime DatumKonsultacija { get; set; }

        [Required]
        public int VrstaZadatkaId { get; set; }

        public string RokDoZavrsetka { get; set; }

        public string Opis { get; set; }

        [ForeignKey("NastavnikId")]
        [InverseProperty("Zadaci")]
        public Nastavnik Nastavnik { get; set; }

        [ForeignKey("StudentId")]
        [InverseProperty("Zadaci")]
        public Student Student { get; set; }

        [ForeignKey("DatumKonsultacija")]
        [InverseProperty("Zadaci")]
        public Datum Datum { get; set; }

        [ForeignKey("VrstaZadatkaId")]
        [InverseProperty("Zadaci")]
        public VrstaZadatka VrstaZadatka { get; set; }
    }
}
