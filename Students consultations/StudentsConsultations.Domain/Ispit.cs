﻿using StudentsConsultations.Data.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsConsultations.Data.Domain
{
    public class Ispit
    {
        [Key]
        [ForeignKey("Razlog")]
        public int RazlogId { get; set; }

        [Required]
        public string Naziv { get; set; }

        public Razlog Razlog { get; set; }
    }
}
