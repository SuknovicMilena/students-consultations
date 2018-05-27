using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Konsultacije
{
    public class KonsultacijaDto
    {
        public int Id { get; set; }

        public int DanUNedelji { get; set; }

        public DateTime VremeOd { get; set; }

        public DateTime VremeDo { get; set; }

        public int NastavnikId { get; set; }
    }
}
