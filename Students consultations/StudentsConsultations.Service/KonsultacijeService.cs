﻿using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Insert(Konsultacije konsultacije, DateTime datum)
        {
            Datum datumKonsultacija = new Datum();
            datumKonsultacija.DatumKonsultacija = datum;

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

        public List<Konsultacije> GroupKonsultacijeByNastavnikForStudent(int studentId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByStudentId(studentId);

            IEnumerable<IGrouping<object, Konsultacije>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.Nastavnik.Ime });
            IEnumerable<Konsultacije> konsultacije = groups.SelectMany(group => group).OrderBy(x => x.Nastavnik.Ime);
            return konsultacije.ToList();
        }

        public List<Konsultacije> GroupKonsultacijeByStundentForNastavnik(int nastavnikId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByNastavnik(nastavnikId);

            IEnumerable<IGrouping<object, Konsultacije>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.Student.Ime });
            IEnumerable<Konsultacije> konsultacije = groups.SelectMany(group => group).OrderBy(x => x.Student.Ime);
            return konsultacije.ToList();
        }

        public List<Konsultacije> GroupKonsultacijeByDatumForNastavnik(int nastavnikId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByNastavnik(nastavnikId);

            IEnumerable<IGrouping<object, Konsultacije>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.DatumKonsultacija });
            IEnumerable<Konsultacije> konsultacije = groups.SelectMany(group => group);
            return konsultacije.ToList();
        }

        public List<Konsultacije> GroupKonsultacijeByDatumForStudent(int studentId)
        {
            var konsultacijeZaStudenta = GetAllKonsultacijeByStudentId(studentId);

            IEnumerable<IGrouping<object, Konsultacije>> groups = konsultacijeZaStudenta.GroupBy(x => new { x.DatumKonsultacija });
            IEnumerable<Konsultacije> konsultacije = groups.SelectMany(group => group);
            return konsultacije.ToList();
        }

        public List<Konsultacije> GetAllKonsultacijeByStudentId(int studentId)
        {
            var konsultacije = _databaseManager.KonsultacijeRepository.GetAll(x => x.StudentId == studentId).ToList();

            foreach (var k in konsultacije)
            {
                var nastavnik = _databaseManager.NastavnikRepository.GetById(k.NastavnikId);
                var student = _databaseManager.StudentRepository.GetById(k.StudentId);
                var razlog = _databaseManager.RazlogRepository.GetById(k.RazlogId);

                k.Nastavnik = nastavnik;
                k.Student = student;
                k.Razlog = razlog;

                var projekat = _databaseManager.ProjekatRepository.GetById(k.RazlogId);
                if (projekat != null)
                {
                    k.Razlog.Projekat = projekat;
                }

                var ispit = _databaseManager.IspitRepository.GetById(k.RazlogId);
                if (ispit != null)
                {
                    k.Razlog.Ispit = ispit;
                }

                var zavrsniRad = _databaseManager.ZavrsniRadRepository.GetById(k.RazlogId);
                if (zavrsniRad != null)
                {
                    k.Razlog.ZavrsniRad = zavrsniRad;
                }
            }
            return konsultacije;
        }

        public List<Konsultacije> GetAllKonsultacijeByNastavnik(int nastavnikId)
        {
            var konsultacije = _databaseManager.KonsultacijeRepository.GetAll(x => x.NastavnikId == nastavnikId).ToList();

            foreach (var k in konsultacije)
            {
                var nastavnik = _databaseManager.NastavnikRepository.GetById(k.NastavnikId);
                var student = _databaseManager.StudentRepository.GetById(k.StudentId);
                var razlog = _databaseManager.RazlogRepository.GetById(k.RazlogId);

                k.Nastavnik = nastavnik;
                k.Student = student;
                k.Razlog = razlog;

                var projekat = _databaseManager.ProjekatRepository.GetById(k.RazlogId);
                if (projekat != null)
                {
                    k.Razlog.Projekat = projekat;
                }

                var ispit = _databaseManager.IspitRepository.GetById(k.RazlogId);
                if (ispit != null)
                {
                    k.Razlog.Ispit = ispit;
                }

                var zavrsniRad = _databaseManager.ZavrsniRadRepository.GetById(k.RazlogId);
                if (zavrsniRad != null)
                {
                    k.Razlog.ZavrsniRad = zavrsniRad;
                }
            }

            IEnumerable<IGrouping<object, Konsultacije>> groups = konsultacije.GroupBy(x => new { x.Odrzane });
            return groups.SelectMany(group => group).OrderBy(x => x.Odrzane).ToList();
        }

        public void Insert(Konsultacije entity)
        {
            throw new NotImplementedException();
        }

        public List<Konsultacije> SearchByNastavnik(string searchText, int studentId)
        {
            return GetAllKonsultacijeByStudentId(studentId).Where(x => x.Nastavnik.Ime.ToLower().StartsWith(searchText.ToLower())
            || x.Nastavnik.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
        }

        public List<Konsultacije> SearchByStudent(string searchText, int nastavnikId)
        {
            return GetAllKonsultacijeByNastavnik(nastavnikId).Where(x => x.Student.Ime.ToLower().StartsWith(searchText.ToLower())
            || x.Student.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
        }

        public Konsultacije GetKonsultacija(int studentId, int nastavnikId, DateTime datum)
        {

            var nastavnik = _databaseManager.NastavnikRepository.GetById(nastavnikId);
            var student = _databaseManager.StudentRepository.GetById(studentId);


            var konsultacija = _databaseManager.KonsultacijeRepository.GetAll(x => x.NastavnikId == nastavnikId && x.StudentId == studentId && x.DatumKonsultacija.Date == datum.Date).FirstOrDefault();

            konsultacija.Nastavnik = nastavnik;
            konsultacija.Student = student;

            var razlog = _databaseManager.RazlogRepository.GetById(konsultacija.RazlogId);

            konsultacija.Razlog = razlog;


            var projekat = _databaseManager.ProjekatRepository.GetById(konsultacija.RazlogId);
            if (projekat != null)
            {
                konsultacija.Razlog.Projekat = projekat;
            }

            var ispit = _databaseManager.IspitRepository.GetById(konsultacija.RazlogId);
            if (ispit != null)
            {
                konsultacija.Razlog.Ispit = ispit;
            }

            var zavrsniRad = _databaseManager.ZavrsniRadRepository.GetById(konsultacija.RazlogId);
            if (zavrsniRad != null)
            {
                konsultacija.Razlog.ZavrsniRad = zavrsniRad;
            }

            return konsultacija;
        }

        public List<Konsultacije> GeneratePDFForStudent(string searchText, int studentId)
        {
            var konsultacije = new List<Konsultacije>();

            if (!String.IsNullOrEmpty(searchText))
            {
                return konsultacije = GetAllKonsultacijeByStudentId(studentId).Where(x => x.Nastavnik.Ime.ToLower().StartsWith(searchText.ToLower())
            || x.Nastavnik.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
            }
            else
            {
                return konsultacije = GetAllKonsultacijeByStudentId(studentId);
            }
        }

        public List<Konsultacije> GeneratePDFForNastavnik(string searchText, int nastavnikId)
        {
            var konsultacije = new List<Konsultacije>();

            if (!String.IsNullOrEmpty(searchText))
            {
                return konsultacije = GetAllKonsultacijeByNastavnik(nastavnikId).Where(x => x.Student.Ime.ToLower().StartsWith(searchText.ToLower())
            || x.Student.Prezime.ToLower().StartsWith(searchText.ToLower())).ToList();
            }
            else
            {
                return konsultacije = GetAllKonsultacijeByNastavnik(nastavnikId);
            }
        }
    }

}