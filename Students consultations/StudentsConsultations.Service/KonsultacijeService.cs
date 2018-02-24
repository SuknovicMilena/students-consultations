using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class KonsultacijeService : IKonsultacijeService
    {
        private readonly IDatabaseManager _databaseManager;

        public KonsultacijeService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Konsultacije> GetAll()
        {
            return _databaseManager.KonsultacijeRepository.GetAll();
        }

        public Konsultacije GetById(int id)
        {
            return _databaseManager.KonsultacijeRepository.GetById(id);
        }

        public void Insert(Konsultacije konsultacije)
        {
            Datum datumKonsultacija = new Datum();
            datumKonsultacija.DatumKonsultacija = DateTime.UtcNow;

            _databaseManager.DatumRepository.Insert(datumKonsultacija);

            konsultacije.DatumObjekat = datumKonsultacija;

            _databaseManager.KonsultacijeRepository.Insert(konsultacije);
            _databaseManager.SaveChanges();
        }

        public void Update(Konsultacije konsultacije)
        {
            _databaseManager.KonsultacijeRepository.Update(konsultacije);
            _databaseManager.SaveChanges();
        }

        public void Delete(Konsultacije konsultacije)
        {
            _databaseManager.KonsultacijeRepository.Delete(konsultacije);
            _databaseManager.SaveChanges();
        }
    }
}
