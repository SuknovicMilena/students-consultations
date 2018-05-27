using StudentsConsultations.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service.Interfaces
{
    public interface IKonsultacijaService : IBaseService<Konsultacija>
    {
        List<Konsultacija> GetAllByNastavnik(int nastavnikId);
    }
}
