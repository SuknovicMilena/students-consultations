using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Razlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class RazlogProfile : Profile
    {
        public RazlogProfile()
        {
            CreateMap<Razlog, RazlogDto>();

            CreateMap<Razlog, RazlogRowDto>();

            CreateMap<RazlogRequest, Razlog>();
        }
    }
}
