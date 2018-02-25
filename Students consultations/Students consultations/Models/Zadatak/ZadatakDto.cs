using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Zadatak
{
    public class ZadatakDto
    {
        public int Id { get; set; }

        public int NastavnikId { get; set; }

        public int StudentId { get; set; }

        public DateTime DatumKonsultacija { get; set; }

        public int VrstaZadatkaId { get; set; }

        public string RokDoZavrsetka { get; set; }

        public string Opis { get; set; }
    }
}
