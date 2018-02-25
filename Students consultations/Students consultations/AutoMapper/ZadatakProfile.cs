using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Zadatak;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class ZadatakProfile : Profile
    {
        public ZadatakProfile()
        {
            CreateMap<ZadatakRequest, Zadatak>();
        }
    }
}
