using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class VrstaZadatkaService : IVrstaZadatkaService
    {
        private readonly IDatabaseManager _databaseManager;

        public VrstaZadatkaService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<VrstaZadatka> GetAll()
        {
            return _databaseManager.VrstaZadatkaRepository.GetAll();
        }

        public VrstaZadatka GetById(int id)
        {
            return _databaseManager.VrstaZadatkaRepository.GetById(id);
        }

        public void Insert(VrstaZadatka vrstaZadatka)
        {
            _databaseManager.VrstaZadatkaRepository.Insert(vrstaZadatka);
            _databaseManager.SaveChanges();
        }

        public void Update(VrstaZadatka vrstaZadatka)
        {
            _databaseManager.VrstaZadatkaRepository.Update(vrstaZadatka);
            _databaseManager.SaveChanges();
        }

        public void Delete(VrstaZadatka vrstaZadatka)
        {
            _databaseManager.VrstaZadatkaRepository.Delete(vrstaZadatka);
            _databaseManager.SaveChanges();
        }
    }
}
