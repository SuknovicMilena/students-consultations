using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Data.EF.Repositories
{
    public class KonsultacijeRepository : BaseRepository<Konsultacije>, IKonsultacijeRepository
    {
        public KonsultacijeRepository(StudentskeKonsultacijeDbContext context) : base(context)
        {
        }
    }
}
