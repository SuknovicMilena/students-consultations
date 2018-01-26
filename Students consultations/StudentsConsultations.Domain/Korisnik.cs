using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace StudentsConsultations.Domain
{
    public class Korisnik : OpstiEntitet
    {
        [Required]
        public string Ime { get; set; }

        [Required]
        public string Prezime { get; set; }
    }
}
