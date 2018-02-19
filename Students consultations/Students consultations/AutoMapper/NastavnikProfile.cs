using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class NastavnikProfile : Profile
    {
        public NastavnikProfile()
        {
            CreateMap<Nastavnik, NastavnikRowDto>();
        }
    }
}
