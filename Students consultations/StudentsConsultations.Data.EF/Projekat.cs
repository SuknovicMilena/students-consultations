using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace StudentsConsultations.Data.EF
{
    public class Projekat
    {
        [Key]
        [ForeignKey("Razlog")]
        public int RazlogId { get; set; }

        [Required]
        public string NazivProjekta { get; set; }

        [Required]
        public string NazivIspita { get; set; }

        public Razlog Razlog { get; set; }
    }
}
