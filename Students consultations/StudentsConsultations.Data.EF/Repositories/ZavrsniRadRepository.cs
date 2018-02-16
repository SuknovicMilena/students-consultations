using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Data.EF.Repositories
{
    public class ZavrsniRadRepository : BaseRepository<ZavrsniRad>, IZavrsniRadRepository
    {
        public ZavrsniRadRepository(StudentskeKonsultacijeDbContext context) : base(context)
        {
        }
    }
}
