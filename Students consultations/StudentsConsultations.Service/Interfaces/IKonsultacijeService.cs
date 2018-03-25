using System;
using System.Collections.Generic;
using System.Text;
using StudentsConsultations.Data.Domain;

namespace StudentsConsultations.Service.Interfaces
{
    public interface IKonsultacijeService : IBaseService<Konsultacije>
    {
        List<Konsultacije> GetAllKonsultacijeByStudentId(int studentId);

        List<Konsultacije> GroupKonsultacijeByNastavnik(int studentId);

        List<Konsultacije> GroupKonsultacijeByDatum(int studentId);
    }
}
