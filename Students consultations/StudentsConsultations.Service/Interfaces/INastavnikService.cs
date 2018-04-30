using StudentsConsultations.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service.Interfaces
{
    public interface INastavnikService : IBaseService<Nastavnik>
    {
        Nastavnik Authenticate(string korisnickoIme, string lozinka);
    }
}
