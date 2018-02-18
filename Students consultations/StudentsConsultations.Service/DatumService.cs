using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class DatumService : IDatumService
    {
        private readonly IDatabaseManager _databaseManager;

        public DatumService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Datum> GetAll()
        {
            return _databaseManager.DatumRepository.GetAll();
        }

        public Datum GetById(int id)
        {
            return _databaseManager.DatumRepository.GetById(id);
        }

        public void Insert(Datum datum)
        {
            _databaseManager.DatumRepository.Insert(datum);
            _databaseManager.SaveChanges();
        }

        public void Update(Datum datum)
        {
            _databaseManager.DatumRepository.Update(datum);
            _databaseManager.SaveChanges();
        }

        public void Delete(Datum datum)
        {
            _databaseManager.DatumRepository.Delete(datum);
            _databaseManager.SaveChanges();
        }
    }
}
