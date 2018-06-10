using System;
using System.Collections.Generic;
using System.Text;
using StudentsConsultations.Data.Domain;

namespace StudentsConsultations.Service.Interfaces
{
    public interface IStudentKonsultacijaService : IBaseService<StudentKonsultacija>
    {
        List<StudentKonsultacija> GetAllKonsultacijeByStudentId(int studentId);

        List<StudentKonsultacija> GetAllKonsultacijeByNastavnik(int nastavnikId);

        List<StudentKonsultacija> GroupKonsultacijeByNastavnikForStudent(int studentId);

        List<StudentKonsultacija> GroupKonsultacijeByDatumForStudent(int studentId);

        List<StudentKonsultacija> GroupKonsultacijeByDatumForNastavnik(int nastavnikId);

        List<StudentKonsultacija> GroupKonsultacijeByStundentForNastavnik(int nastavnikId);

        void Insert(StudentKonsultacija konsultacije, DateTime datum);

        List<StudentKonsultacija> SearchByNastavnik(string searchText, int studentId);

        List<StudentKonsultacija> SearchByStudent(string searchText, int nastavnikId);

        List<StudentKonsultacija> GeneratePDFForStudent(string searchText, int nastavnikId);

        List<StudentKonsultacija> GeneratePDFForNastavnik(string searchText, int nastavnikId);

        StudentKonsultacija GetKonsultacija(int id);

        List<StudentKonsultacija> GetAllZakazaneKonsultacijeKonsultacijeByNastavnik(int nastavnikId, DateTime zeljeniDatum);
    }
}
