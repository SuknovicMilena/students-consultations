﻿using Microsoft.EntityFrameworkCore;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Data.EF.Repositories
{
    public class IspitRepository : BaseRepository<Ispit>, IIspitRepository
    {
        public IspitRepository(StudentskeKonsultacijeDbContext contex) : base(contex)
        {
        }
    }
}
