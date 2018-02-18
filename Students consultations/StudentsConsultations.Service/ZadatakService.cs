using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class ZadatakService : IZadatakService
    {
        private readonly IDatabaseManager _databaseManager;

        public ZadatakService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Zadatak> GetAll()
        {
            return _databaseManager.ZadatakRepository.GetAll();
        }

        public Zadatak GetById(int id)
        {
            return _databaseManager.ZadatakRepository.GetById(id);
        }

        public void Insert(Zadatak zadatak)
        {
            _databaseManager.ZadatakRepository.Insert(zadatak);
            _databaseManager.SaveChanges();
        }

        public void Update(Zadatak zadatak)
        {
            _databaseManager.ZadatakRepository.Update(zadatak);
            _databaseManager.SaveChanges();
        }

        public void Delete(Zadatak zadatak)
        {
            _databaseManager.ZadatakRepository.Delete(zadatak);
            _databaseManager.SaveChanges();
        }
    }
}
