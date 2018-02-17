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

        public void Delete(Datum entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Datum> GetAll()
        {
            throw new NotImplementedException();
        }

        public Datum GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Datum entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Datum entity)
        {
            throw new NotImplementedException();
        }
    }
}
