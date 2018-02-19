using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class ZavrsniRadService : IZavrsniRadService
    {
        private readonly IDatabaseManager _databaseManager;

        public ZavrsniRadService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<ZavrsniRad> GetAll()
        {
            return _databaseManager.ZavrsniRadRepository.GetAll();
        }

        public ZavrsniRad GetById(int id)
        {
            return _databaseManager.ZavrsniRadRepository.GetById(id);
        }

        public void Insert(ZavrsniRad zavrsniRad)
        {
            _databaseManager.ZavrsniRadRepository.Insert(zavrsniRad);
            _databaseManager.SaveChanges();
        }

        public void Update(ZavrsniRad zavrsniRad)
        {
            _databaseManager.ZavrsniRadRepository.Insert(zavrsniRad);
            _databaseManager.SaveChanges();
        }

        public void Delete(ZavrsniRad zavrsniRad)
        {
            _databaseManager.ZavrsniRadRepository.Insert(zavrsniRad);
            _databaseManager.SaveChanges();
        }
    }
}
