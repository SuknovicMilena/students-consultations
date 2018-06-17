using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace StudentsConsultations.Service
{
    public class StudentKonsultacijaService : IStudentKonsultacijaService
    {
        private readonly IDatabaseManager _databaseManager;

        public StudentKonsultacijaService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<StudentKonsultacija> GetAll()
        {
            return _databaseManager.StudentKonsultacijaRepository.GetAll();
        }

        public StudentKonsultacija GetById(int id)
        {
            return _databaseManager.StudentKonsultacijaRepository.GetById(id);
        }

        public void Insert(StudentKonsultacija konsultacije, DateTime datum)
        {
            //Datum datumKonsultacija = new Datum();
            //datumKonsultacija.DatumKonsultacija = datum;

            //_databaseManager.DatumRepository.Insert(datumKonsultacija);

            //konsultacije.DatumObjekat = datumKonsultacija;

            //_databaseManager.KonsultacijeRepository.Insert(konsultacije);
            _databaseManager.SaveChanges();
        }

        public void Update(StudentKonsultacija konsultacije)
        {
            _databaseManager.StudentKonsultacijaRepository.Update(konsultacije);
            _databaseManager.SaveChanges();
        }

        public void Delete(StudentKonsultacija konsultacije)
        {
            _databaseManager.StudentKonsultacijaRepository.Delete(konsultacije);
            _databaseManager.SaveChanges();
        }

        public List<StudentKonsultacija> GroupKonsultacijeByNastavnikForStudent(int studentId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByStudentId(studentId);

            IEnumerable<IGrouping<object, StudentKonsultacija>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.Nastavnik.Ime });
            IEnumerable<StudentKonsultacija> konsultacije = groups.SelectMany(group => group).OrderBy(x => x.Nastavnik.Ime);
            return konsultacije.ToList();
        }

        public List<StudentKonsultacija> GroupKonsultacijeByStundentForNastavnik(int nastavnikId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByNastavnik(nastavnikId);

            IEnumerable<IGrouping<object, StudentKonsultacija>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.Student.Ime });
            IEnumerable<StudentKonsultacija> konsultacije = groups.SelectMany(group => group).OrderBy(x => x.Student.Ime);
            return konsultacije.ToList();
        }

        public List<StudentKonsultacija> GroupKonsultacijeByDatumForNastavnik(int nastavnikId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByNastavnik(nastavnikId);

            IEnumerable<IGrouping<object, StudentKonsultacija>> groups = konsultacijeZaStudenta.GroupBy(x => new { });
            IEnumerable<StudentKonsultacija> konsultacije = groups.SelectMany(group => group);
            return konsultacije.ToList();
        }

        public List<StudentKonsultacija> GroupKonsultacijeByDatumForStudent(int studentId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByStudentId(studentId);

            IEnumerable<IGrouping<object, StudentKonsultacija>> groups = konsultacijeZaStudenta.GroupBy(x => new { });
            IEnumerable<StudentKonsultacija> konsultacije = groups.SelectMany(group => group);
            return konsultacije.ToList();
        }

        public List<StudentKonsultacija> GetAllKonsultacijeByStudentId(int studentId)
        {
            var konsultacije = _databaseManager.StudentKonsultacijaRepository.GetAll(x => x.StudentId == studentId).ToList();

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

                var ispit = _databaseManager.IspitRepository.GetById(k.RazlogId);
                if (ispit != null)
                {
                    k.Razlog.Ispit = ispit;
                }

                var zavrsniRad = _databaseManager.ZavrsniRadRepository.GetById(k.RazlogId);
                if (zavrsniRad != null)
                {
                    k.Razlog.ZavrsniRad = zavrsniRad;
                }
            }
            return konsultacije;
        }

        public List<StudentKonsultacija> GetAllZakazaneKonsultacijeKonsultacijeByNastavnik(int nastavnikId, DateTime zeljeniDatum)
        {
            return _databaseManager.StudentKonsultacijaRepository.GetAll(x => x.NastavnikId == nastavnikId && x.VremeOd.Date == zeljeniDatum.Date).ToList();
        }

        public List<StudentKonsultacija> GetAllKonsultacijeByNastavnik(int nastavnikId)
        {
            var konsultacije = _databaseManager.StudentKonsultacijaRepository.GetAll(x => x.NastavnikId == nastavnikId).ToList();

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

                var ispit = _databaseManager.IspitRepository.GetById(k.RazlogId);
                if (ispit != null)
                {
                    k.Razlog.Ispit = ispit;
                }

                var zavrsniRad = _databaseManager.ZavrsniRadRepository.GetById(k.RazlogId);
                if (zavrsniRad != null)
                {
                    k.Razlog.ZavrsniRad = zavrsniRad;
                }
            }

            IEnumerable<IGrouping<object, StudentKonsultacija>> groups = konsultacije.GroupBy(x => new { x.Odrzane });
            return groups.SelectMany(group => group).OrderBy(x => x.Odrzane).ToList();
        }

        public void Insert(StudentKonsultacija konsultacija)
        {
            _databaseManager.StudentKonsultacijaRepository.Insert(konsultacija);
            _databaseManager.SaveChanges();
        }

        public List<StudentKonsultacija> SearchByNastavnikAndDate(string searchText, int studentId)
        {
            DateTime datetime;

            bool checkIfIsDate = DateTime.TryParseExact(searchText, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetime);

            if (checkIfIsDate)
            {
                return GetAllKonsultacijeByStudentId(studentId).Where(x => x.VremeOd.Date == DateTime.ParseExact(searchText, "dd-MM-yyyy", null)).ToList();
            }
            else
            {
                return GetAllKonsultacijeByStudentId(studentId).Where(x => x.Nastavnik.Ime.ToLower().StartsWith(searchText.ToLower())
          || x.Nastavnik.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
            }
        }

        public List<StudentKonsultacija> SearchByStudentAndDate(string searchText, int nastavnikId)
        {
            DateTime datetime;

            bool checkIfIsDate = DateTime.TryParseExact(searchText, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetime);

            if (checkIfIsDate)
            {
                return GetAllKonsultacijeByNastavnik(nastavnikId).Where(x => x.VremeOd.Date == DateTime.ParseExact(searchText, "dd-MM-yyyy", null)).ToList();
            }
            else
            {
                return GetAllKonsultacijeByNastavnik(nastavnikId).Where(x => x.Student.Ime.ToLower().StartsWith(searchText.ToLower())
            || x.Student.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
            }
        }

        public StudentKonsultacija GetKonsultacija(int id)
        {

            //var nastavnik = _databaseManager.NastavnikRepository.GetById(nastavnikId);
            //var student = _databaseManager.StudentRepository.GetById(studentId);


            var konsultacija = _databaseManager.StudentKonsultacijaRepository.GetAll(x => x.Id == id).FirstOrDefault();

            //konsultacija.Nastavnik = nastavnik;
            //konsultacija.Student = student;

            var razlog = _databaseManager.RazlogRepository.GetById(konsultacija.RazlogId);

            konsultacija.Razlog = razlog;


            var projekat = _databaseManager.ProjekatRepository.GetById(konsultacija.RazlogId);
            if (projekat != null)
            {
                konsultacija.Razlog.Projekat = projekat;
            }

            var ispit = _databaseManager.IspitRepository.GetById(konsultacija.RazlogId);
            if (ispit != null)
            {
                konsultacija.Razlog.Ispit = ispit;
            }

            var zavrsniRad = _databaseManager.ZavrsniRadRepository.GetById(konsultacija.RazlogId);
            if (zavrsniRad != null)
            {
                konsultacija.Razlog.ZavrsniRad = zavrsniRad;
            }

            return konsultacija;
        }

        public List<StudentKonsultacija> GeneratePDFForStudent(string searchText, int studentId)
        {
            var konsultacije = new List<StudentKonsultacija>();

            if (!String.IsNullOrEmpty(searchText))
            {
                DateTime datetime;

                bool checkIfIsDate = DateTime.TryParseExact(searchText, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetime);

                if (checkIfIsDate)
                {
                    return GetAllKonsultacijeByStudentId(studentId).Where(x => x.VremeOd.Date == DateTime.ParseExact(searchText, "dd-MM-yyyy", null)).ToList();
                }
                else
                {
                    return GetAllKonsultacijeByStudentId(studentId).Where(x => x.Nastavnik.Ime.ToLower().StartsWith(searchText.ToLower())
              || x.Nastavnik.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
                }
            }
            else
            {
                return konsultacije = GetAllKonsultacijeByStudentId(studentId);
            }
        }

        public List<StudentKonsultacija> GeneratePDFForNastavnik(string searchText, int nastavnikId)
        {
            var konsultacije = new List<StudentKonsultacija>();

            if (!String.IsNullOrEmpty(searchText))
            {
                DateTime datetime;

                bool checkIfIsDate = DateTime.TryParseExact(searchText, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out datetime);

                if (checkIfIsDate)
                {
                    return GetAllKonsultacijeByNastavnik(nastavnikId).Where(x => x.VremeOd.Date == DateTime.ParseExact(searchText, "dd-MM-yyyy", null)).ToList();
                }
                else
                {
                    return GetAllKonsultacijeByNastavnik(nastavnikId).Where(x => x.Student.Ime.ToLower().StartsWith(searchText.ToLower())
                || x.Student.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
                }
            }
            else
            {
                return konsultacije = GetAllKonsultacijeByNastavnik(nastavnikId);
            }
        }
    }

}