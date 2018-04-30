using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class NastavnikService : INastavnikService
    {
        private readonly IDatabaseManager _databaseManager;

        public NastavnikService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public Nastavnik Authenticate(string korisnickoIme, string lozinka)
        {
            if (string.IsNullOrEmpty(korisnickoIme) || string.IsNullOrEmpty(lozinka))
                return null;

            var nastavnik = _databaseManager.NastavnikRepository.GetBy(x => x.KorisnickoIme == korisnickoIme);

            if (nastavnik == null)
                return null;

            return nastavnik;
        }

        public IEnumerable<Nastavnik> GetAll()
        {
            return _databaseManager.NastavnikRepository.GetAll();
        }

        public Nastavnik GetById(int id)
        {
            return _databaseManager.NastavnikRepository.GetById(id);
        }

        public void Insert(Nastavnik nastavnik)
        {
            _databaseManager.NastavnikRepository.Insert(nastavnik);
            _databaseManager.SaveChanges();
        }

        public void Update(Nastavnik nastavnik)
        {
            _databaseManager.NastavnikRepository.Update(nastavnik);
            _databaseManager.SaveChanges();
        }

        public void Delete(Nastavnik nastavnik)
        {
            _databaseManager.NastavnikRepository.Delete(nastavnik);
            _databaseManager.SaveChanges();
        }
    }
}
