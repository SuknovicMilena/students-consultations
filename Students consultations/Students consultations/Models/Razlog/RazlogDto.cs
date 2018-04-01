using StudentsConsultations.Models.Ispit;
using StudentsConsultations.Models.Projekat;
using StudentsConsultations.Models.ZavrsniRad;
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

        public IspitDto Ispit { get; set; }

        public ZavrsniRadDto ZavrsniRad { get; set; }

        public ProjekatDto Projekat { get; set; }
    }
}
