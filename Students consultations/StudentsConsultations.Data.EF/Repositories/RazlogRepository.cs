using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Data.EF.Repositories
{
    public class RazlogRepository : BaseRepository<Razlog>, IRazlogRepository
    {
        public RazlogRepository(StudentskeKonsultacijeDbContext context) : base(context)
        {
        }
    }
}
