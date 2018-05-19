using System;
using System.Collections.Generic;
using System.Text;
using StudentsConsultations.Data.Domain;

namespace StudentsConsultations.Service.Interfaces
{
    public interface IKonsultacijeService : IBaseService<Konsultacije>
    {
        List<Konsultacije> GetAllKonsultacijeByStudentId(int studentId);

        List<Konsultacije> GetAllKonsultacijeByNastavnik(int nastavnikId);

        List<Konsultacije> GroupKonsultacijeByNastavnikForStudent(int studentId);

        List<Konsultacije> GroupKonsultacijeByDatumForStudent(int studentId);

        List<Konsultacije> GroupKonsultacijeByDatumForNastavnik(int nastavnikId);

        List<Konsultacije> GroupKonsultacijeByStundentForNastavnik(int nastavnikId);

        void Insert(Konsultacije konsultacije, DateTime datum);

        List<Konsultacije> SearchByNastavnik(string searchText, int studentId);

        List<Konsultacije> SearchByStudent(string searchText, int nastavnikId);

        List<Konsultacije> GeneratePDFForStudent(string searchText, int nastavnikId);

        List<Konsultacije> GeneratePDFForNastavnik(string searchText, int nastavnikId);

        Konsultacije GetKonsultacija(int studentId, int nastavnikId, DateTime datum);
    }
}
