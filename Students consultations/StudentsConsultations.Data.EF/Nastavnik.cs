using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsConsultations.Data.EF
{
    public class Nastavnik : Korisnik
    {
        [Required]
        public string BrojRadneKnjizice { get; set; }
    }
}
