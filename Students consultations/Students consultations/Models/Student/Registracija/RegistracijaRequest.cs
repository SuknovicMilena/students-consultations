using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Models.Student.Registracija
{
    public class RegistracijaRequest
    {
        public string Ime { get; set; }

        public string Prezime { get; set; }

        public string BrojIndeksa { get; set; }

        public string KorisnickoIme { get; set; }

        public string Lozinka { get; set; }
    }
}
