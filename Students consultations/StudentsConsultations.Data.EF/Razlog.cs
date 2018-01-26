using System;
using System.Collections.Generic;

namespace StudentsConsultations.Data.EF
{
    public class Razlog
    {
        public int Id { get; set; }
        public string Opis { get; set; }

        public ICollection<Konsultacije> Konsultacije { get; set; } = new HashSet<Konsultacije>();
    }
}
