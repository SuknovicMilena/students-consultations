﻿using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.EF.Repositories;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StudentsConsultations.Data.EF
{
    public class DatabaseManager : IDatabaseManager
    {
        private StudentskeKonsultacijeDbContext _context;

        private bool _disposed;

        private IIspitRepository _ispitRepository;

        private IStudentKonsultacijaRepository _studentKonsultacijaRepository;

        private IKonsultacijaRepository _konsultacijaRepository;

        private INastavnikRepository _nastavnikRepository;

        private IProjekatRepository _projekatRepository;

        private IRazlogRepository _razlogRepository;

        private IStudentRepository _studentRepository;

        private IVrstaZadatkaRepository _vrstaZadatkaRepository;

        private IZadatakRepository _zadatakRepository;

        private IZavrsniRadRepository _zavrsniRadRepository;

        public IIspitRepository IspitRepository => _ispitRepository ?? (_ispitRepository = new IspitRepository(_context));

        public IStudentKonsultacijaRepository StudentKonsultacijaRepository => _studentKonsultacijaRepository ?? (_studentKonsultacijaRepository = new StudentKonsultacijaRepository(_context));

        public IKonsultacijaRepository KonsultacijaRepository => _konsultacijaRepository ?? (_konsultacijaRepository = new KonsultacijaRepository(_context));

        public INastavnikRepository NastavnikRepository => _nastavnikRepository ?? (_nastavnikRepository = new NastavnikRepository(_context));

        public IProjekatRepository ProjekatRepository => _projekatRepository ?? (_projekatRepository = new ProjekatRepository(_context));

        public IRazlogRepository RazlogRepository => _razlogRepository ?? (_razlogRepository = new RazlogRepository(_context));

        public IStudentRepository StudentRepository => _studentRepository ?? (_studentRepository = new StudentRepository(_context));

        public IVrstaZadatkaRepository VrstaZadatkaRepository => _vrstaZadatkaRepository ?? (_vrstaZadatkaRepository = new VrstaZadatkaRepository(_context));

        public IZadatakRepository ZadatakRepository => _zadatakRepository ?? (_zadatakRepository = new ZadatakRepository(_context));

        public IZavrsniRadRepository ZavrsniRadRepository => _zavrsniRadRepository ?? (_zavrsniRadRepository = new ZavrsniRadRepository(_context));


        public DatabaseManager(StudentskeKonsultacijeDbContext contex)
        {
            _context = contex;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();

        }
    }

}