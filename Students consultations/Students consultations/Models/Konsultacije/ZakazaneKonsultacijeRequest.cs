using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Konsultacije
{
    public class ZakazaneKonsultacijeRequest
    {
        public DateTime ZeljeniDatum { get; set; }

        public int NastavnikId { get; set; }
    }
}
