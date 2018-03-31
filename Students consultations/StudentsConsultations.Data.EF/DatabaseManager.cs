using Microsoft.EntityFrameworkCore;
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

        private IDatumRepository _datumRepository;

        private IIspitRepository _ispitRepository;

        private IKonsultacijeRepository _konsultacijeRepository;

        private INastavnikRepository _nastavnikRepository;

        private IProjekatRepository _projekatRepository;

        private IRazlogRepository _razlogRepository;

        private IStudentRepository _studentRepository;

        private IVrstaZadatkaRepository _vrstaZadatkaRepository;

        private IZadatakRepository _zadatakRepository;

        private IZavrsniRadRepository _zavrsniRadRepository;

        public IDatumRepository DatumRepository => _datumRepository ?? (_datumRepository = new DatumRepository(_context));

        public IIspitRepository IspitRepository => _ispitRepository ?? (_ispitRepository = new IspitRepository(_context));

        public IKonsultacijeRepository KonsultacijeRepository => _konsultacijeRepository ?? (_konsultacijeRepository = new KonsultacijeRepository(_context));

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
            try
            {
                // Attempt to save changes to the database
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                foreach (var entry in ex.Entries)
                {
                    if (entry.Entity is Konsultacije)
                    {
                        // Using a NoTracking query means we get the entity but it is not tracked by the context
                        // and will not be merged with existing entities in the context.
                        var databaseEntity = _context.Konsultacije.AsNoTracking().Single(p => p.StudentId == ((Konsultacije)entry.Entity).StudentId);
                        var databaseEntry = _context.Entry(databaseEntity);

                        foreach (var property in entry.Metadata.GetProperties())
                        {
                            var proposedValue = entry.Property(property.Name).CurrentValue;

                            // TODO: Logic to decide which value should be written to database
                            entry.Property(property.Name).CurrentValue = proposedValue;

                            // Update original values to 
                            entry.Property(property.Name).OriginalValue = databaseEntry.Property(property.Name).CurrentValue;
                        }
                    }
                    else
                    {
                        throw new NotSupportedException("Don't know how to handle concurrency conflicts for " + entry.Metadata.Name);
                    }
                }

                // Retry the save operation
                _context.SaveChanges();

            }
        }
    }

}