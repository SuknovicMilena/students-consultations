using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Data.EF.Repositories
{
    public class ZadatakRepository : BaseRepository<Zadatak>, IZadatakRepository
    {
        public ZadatakRepository(StudentskeKonsultacijeDbContext context) : base(context)
        {
        }
    }
}
