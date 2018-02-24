using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.VrstaZadatka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class VrstaZadatkaProfile : Profile
    {
        public VrstaZadatkaProfile()
        {
            CreateMap<VrstaZadatkaRequest, VrstaZadatka>();

            CreateMap<VrstaZadatka, VrstaZadatkaDto>();

            CreateMap<VrstaZadatka, VrstaZadatkaRowDto>();
        }
    }
}
