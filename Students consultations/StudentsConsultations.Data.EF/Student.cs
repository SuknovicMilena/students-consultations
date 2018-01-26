using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StudentsConsultations.Data.EF
{
    public class Student : Korisnik
    {
        [Required]
        public string BrojIndeksa { get; set; }
    }
}
