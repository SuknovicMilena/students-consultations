using StudentsConsultations.Data.EF.Repositories;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Data.EF
{
    public class DatabaseManager : IDatabaseManager
    {
        private StudentskeKonsultacijeDbContext _contex;

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


        public IDatumRepository DatumRepository => _datumRepository ?? (_datumRepository = new DatumRepository(_contex));

        public IIspitRepository IspitRepository => _ispitRepository ?? (_ispitRepository = new IspitRepository(_contex));

        public IKonsultacijeRepository KonsultacijeRepository => _konsultacijeRepository ?? (_konsultacijeRepository = new KonsultacijeRepository(_contex));

        public INastavnikRepository NastavnikRepository => _nastavnikRepository ?? (_nastavnikRepository = new NastavnikRepository(_contex));

        public IProjekatRepository ProjekatRepository => _projekatRepository ?? (_projekatRepository = new ProjekatRepository(_contex));

        public IRazlogRepository RazlogRepository => _razlogRepository ?? (_razlogRepository = new RazlogRepository(_contex));

        public IStudentRepository StudentRepository => _studentRepository ?? (_studentRepository = new StudentRepository(_contex));

        public IVrstaZadatkaRepository VrstaZadatkaRepository => _vrstaZadatkaRepository ?? (_vrstaZadatkaRepository = new VrstaZadatkaRepository(_contex));

        public IZadatakRepository ZadatakRepository => _zadatakRepository ?? (_zadatakRepository = new ZadatakRepository(_contex));

        public IZavrsniRadRepository ZavrsniRadRepository => _zavrsniRadRepository ?? (_zavrsniRadRepository = new ZavrsniRadRepository(_contex));

        public DatabaseManager(StudentskeKonsultacijeDbContext contex)
        {
            _contex = contex;
        }
    }
}
