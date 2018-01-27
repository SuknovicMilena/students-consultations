using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentsConsultations.Domain
{
    public class VrstaZadatka
    {

        public int Id { get; set; }

        [Required]
        public string Naziv { get; set; }

        [InverseProperty("VrstaZadatka")]
        public List<Zadatak> Zadaci { get; set; }
    }
}
