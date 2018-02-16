using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace StudentsConsultations.Data.EF.Repositories
{
    public class DatumRepository : BaseRepository<Datum>, IDatumRepository
    {
        public DatumRepository(StudentskeKonsultacijeDbContext context) : base(context)
        {

        }
    }
}
