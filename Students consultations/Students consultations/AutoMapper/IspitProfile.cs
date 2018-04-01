using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Ispit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class IspitProfile : Profile
    {
        public IspitProfile()
        {
            CreateMap<Ispit, IspitDto>();
        }
    }
}
