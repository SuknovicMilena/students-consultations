using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Zadatak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class ZavrsniRadProfile : Profile
    {
        public ZavrsniRadProfile()
        {
            CreateMap<Zadatak, ZadatakDto>();
        }
    }
}
