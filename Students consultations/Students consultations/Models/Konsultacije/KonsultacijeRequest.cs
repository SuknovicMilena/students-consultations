using Newtonsoft.Json;
using StudentsConsultations.Models.Nastavnik;
using StudentsConsultations.Models.Razlog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Konsultacije
{
    public class KonsultacijeRequest
    {
        public int StudentId { get; set; }

        public int NastavnikId { get; set; }

        public int RazlogId { get; set; }

        public bool Odrzane { get; set; }

        public string DatumString { get; set; }

        public DateTime DatumKonsultacija { get; set; }

        public RazlogRequest Razlog { get; set; }
    }
}
