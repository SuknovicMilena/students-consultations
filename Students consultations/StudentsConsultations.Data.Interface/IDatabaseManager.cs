using StudentsConsultations.Data.Interface.Repositories;
using System;

namespace StudentsConsultations.Data.Interface
{
    public interface IDatabaseManager : IDisposable
    {
        IIspitRepository IspitRepository { get; }

        IStudentKonsultacijaRepository StudentKonsultacijaRepository { get; }

        IKonsultacijaRepository KonsultacijaRepository { get; }

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
