using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsConsultations.Service
{
    public class KonsultacijaService : IKonsultacijaService
    {
        private readonly IDatabaseManager _databaseManager;

        public KonsultacijaService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Konsultacija> GetAll()
        {
            return _databaseManager.KonsultacijaRepository.GetAll();
        }

        public Konsultacija GetById(int id)
        {
            return _databaseManager.KonsultacijaRepository.GetById(id);
        }

        public Konsultacija GetByNastavnikIdAndDanUNedelji(int nastavnikId, int danUNedelji)
        {
            return _databaseManager.KonsultacijaRepository.GetBy(x => x.NastavnikId == nastavnikId && x.DanUNedelji == danUNedelji);
        }

        public List<Konsultacija> GetAllByNastavnik(int nastavnikId)
        {
            return _databaseManager.KonsultacijaRepository.GetAll(x => x.NastavnikId == nastavnikId).ToList();
        }

        public void Insert(Konsultacija konsultacija)
        {
            _databaseManager.KonsultacijaRepository.Insert(konsultacija);
            _databaseManager.SaveChanges();
        }

        public void Update(Konsultacija konsultacija)
        {
            _databaseManager.KonsultacijaRepository.Update(konsultacija);
            _databaseManager.SaveChanges();
        }

        public void Delete(Konsultacija konsultacija)
        {
            _databaseManager.KonsultacijaRepository.Delete(konsultacija);
            _databaseManager.SaveChanges();
        }
    }
}
