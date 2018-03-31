using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Konsultacije
{
    public class KonsultacijeRequest
    {
        public int StudentId { get; set; }

        public int NastavnikId { get; set; }

        public int RazlogId { get; set; }

        public bool Odrzane { get; set; }
    }
}
