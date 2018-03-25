using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsConsultations.Service
{
    public class KonsultacijeService : IKonsultacijeService
    {
        private readonly IDatabaseManager _databaseManager;

        public KonsultacijeService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Konsultacije> GetAll()
        {
            return _databaseManager.KonsultacijeRepository.GetAll();
        }

        public Konsultacije GetById(int id)
        {
            return _databaseManager.KonsultacijeRepository.GetById(id);
        }

        public void Insert(Konsultacije konsultacije)
        {
            Datum datumKonsultacija = new Datum();
            datumKonsultacija.DatumKonsultacija = DateTime.UtcNow;

            _databaseManager.DatumRepository.Insert(datumKonsultacija);

            konsultacije.DatumObjekat = datumKonsultacija;

            _databaseManager.KonsultacijeRepository.Insert(konsultacije);
            _databaseManager.SaveChanges();
        }

        public void Update(Konsultacije konsultacije)
        {
            _databaseManager.KonsultacijeRepository.Update(konsultacije);
            _databaseManager.SaveChanges();
        }

        public void Delete(Konsultacije konsultacije)
        {
            _databaseManager.KonsultacijeRepository.Delete(konsultacije);
            _databaseManager.SaveChanges();
        }

        public List<Konsultacije> GroupKonsultacijeByNastavnik(int studentId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByStudentId(studentId);

            IEnumerable<IGrouping<object, Konsultacije>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.Nastavnik.Ime });
            IEnumerable<Konsultacije> konsultacije = groups.SelectMany(group => group).OrderBy(x => x.Nastavnik.Ime);
            return konsultacije.ToList();
        }
        public List<Konsultacije> GroupKonsultacijeByDatum(int studentId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByStudentId(studentId);

            IEnumerable<IGrouping<object, Konsultacije>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.Nastavnik.Ime });
            IEnumerable<Konsultacije> konsultacije = groups.SelectMany(group => group);
            return konsultacije.ToList();
        }

        public List<Konsultacije> GetAllKonsultacijeByStudentId(int studentId)
        {
            var konsultacije = _databaseManager.KonsultacijeRepository.GetAll(x => x.StudentId == studentId).ToList();

            foreach (var k in konsultacije)
            {
                var nastavnik = _databaseManager.NastavnikRepository.GetById(k.NastavnikId);
                var student = _databaseManager.StudentRepository.GetById(k.StudentId);
                var razlog = _databaseManager.RazlogRepository.GetById(k.RazlogId);

                k.Nastavnik = nastavnik;
                k.Student = student;
                k.Razlog = razlog;

                var projekat = _databaseManager.ProjekatRepository.GetById(k.RazlogId);
                if (projekat != null)
                {
                    k.Razlog.Projekat = projekat;
                }
            }
            return konsultacije;
        }
    }

}