using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Razlog
{
    public class RazlogRequest
    {
        public int RazlogId { get; set; }

        public string Opis { get; set; }

        public RazlogType Type { get; set; }

        public string NazivTipa { get; set; }

        public string NazivIspita { get; set; }

        public string TipZavrsnogRada { get; set; }
    }
}
