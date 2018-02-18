using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class IspitService : IIspitService
    {
        private readonly IDatabaseManager _databaseManager;

        public IspitService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Ispit> GetAll()
        {
            return _databaseManager.IspitRepository.GetAll();
        }

        public Ispit GetById(int id)
        {
            return _databaseManager.IspitRepository.GetById(id);
        }

        public void Insert(Ispit ispit)
        {
            _databaseManager.IspitRepository.Insert(ispit);
            _databaseManager.SaveChanges();
        }

        public void Update(Ispit ispit)
        {
            _databaseManager.IspitRepository.Update(ispit);
            _databaseManager.SaveChanges();
        }

        public void Delete(Ispit ispit)
        {
            _databaseManager.IspitRepository.Delete(ispit);
            _databaseManager.SaveChanges();
        }
    }
}
