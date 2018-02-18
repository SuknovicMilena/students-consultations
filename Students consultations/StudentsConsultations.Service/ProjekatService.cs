using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class ProjekatService : IProjekatService
    {
        private readonly IDatabaseManager _databaseManager;

        public ProjekatService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Projekat> GetAll()
        {
            return _databaseManager.ProjekatRepository.GetAll();
        }

        public Projekat GetById(int id)
        {
            return _databaseManager.ProjekatRepository.GetById(id);
        }

        public void Insert(Projekat projekat)
        {
            _databaseManager.ProjekatRepository.Insert(projekat);
            _databaseManager.SaveChanges();
        }

        public void Update(Projekat projekat)
        {
            _databaseManager.ProjekatRepository.Update(projekat);
            _databaseManager.SaveChanges();
        }

        public void Delete(Projekat projekat)
        {
            _databaseManager.ProjekatRepository.Delete(projekat);
            _databaseManager.SaveChanges();
        }
    }
}
