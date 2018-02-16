using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Data.EF.Repositories
{
    public class VrstaZadatkaRepository : BaseRepository<VrstaZadatka>, IVrstaZadatkaRepository
    {
        public VrstaZadatkaRepository(StudentskeKonsultacijeDbContext context) : base(context)
        {
        }
    }
}
