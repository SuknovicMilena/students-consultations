using StudentsConsultations.Models.Razlog;
using System;

namespace StudentsConsultations.Models.Konsultacije
{
    public class KonsultacijaRequest
    {
        public int Id { get; set; }

        public int DanUNedelji { get; set; }

        public string VremeOd { get; set; }

        public string VremeDo { get; set; }

        public int NastavnikId { get; set; }
    }
}
