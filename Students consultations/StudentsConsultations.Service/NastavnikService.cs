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
