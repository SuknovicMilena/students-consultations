using StudentsConsultations.Models.Projekat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Razlog
{
    public class RazlogDto
    {
        public int RazlogId { get; set; }

        public string Opis { get; set; }

        public ProjekatDto Projekat { get; set; }
    }
}
