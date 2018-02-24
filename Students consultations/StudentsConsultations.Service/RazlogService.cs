using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class RazlogService : IRazlogService
    {
        private readonly IDatabaseManager _databaseManager;

        public RazlogService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Razlog> GetAll()
        {
            return _databaseManager.RazlogRepository.GetAll();
        }

        public Razlog GetById(int id)
        {
            return _databaseManager.RazlogRepository.GetById(id);
        }

        public void Insert(Razlog razlog)
        {
            _databaseManager.RazlogRepository.Insert(razlog);
            _databaseManager.SaveChanges();
        }

        public void Update(Razlog razlog)
        {
            _databaseManager.RazlogRepository.Update(razlog);
            _databaseManager.SaveChanges();
        }

        public void Delete(Razlog razlog)
        {
            _databaseManager.RazlogRepository.Delete(razlog);
            _databaseManager.SaveChanges();
        }

        public int InsertAndReturnId(Razlog razlog)
        {
            _databaseManager.RazlogRepository.Insert(razlog);
            _databaseManager.SaveChanges();

            return razlog.RazlogId;
        }
    }
}
