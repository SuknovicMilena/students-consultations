using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Razlog : OpstiEntitet
    {
        public string Opis { get; set; }

        [InverseProperty("Razlog")]
        public List<Konsultacije> Konsultacije { get; set; }
    }
}
