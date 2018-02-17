using StudentsConsultations.Data.Interface.Repositories;
using System;

namespace StudentsConsultations.Data.Interface
{
    public interface IDatabaseManager : IDisposable
    {
        IDatumRepository DatumRepository { get; }

        IIspitRepository IspitRepository { get; }

        IKonsultacijeRepository KonsultacijeRepository { get; }

        INastavnikRepository NastavnikRepository { get; }

        IProjekatRepository ProjekatRepository { get; }

        IRazlogRepository RazlogRepository { get; }

        IStudentRepository StudentRepository { get; }

        IVrstaZadatkaRepository VrstaZadatkaRepository { get; }

        IZadatakRepository ZadatakRepository { get; }

        IZavrsniRadRepository ZavrsniRadRepository { get; }

        void SaveChanges();
    }
}
