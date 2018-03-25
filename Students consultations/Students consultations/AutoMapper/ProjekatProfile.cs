using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Projekat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class ProjekatProfile : Profile
    {
        public ProjekatProfile()
        {
            CreateMap<Projekat, ProjekatDto>();
        }

    }
}
