using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Nastavnik.Registracija
{
    public class RegistracijaRequest
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string BrojRadneKnjizice { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }
    }
}
