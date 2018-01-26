using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class VrstaZadatka : OpstiEntitet
    {
        public string Naziv { get; set; }

        [InverseProperty("VrstaZadatka")]
        public List<Zadatak> Zadaci { get; set; }
    }
}
