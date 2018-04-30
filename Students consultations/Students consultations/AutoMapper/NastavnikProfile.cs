using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Nastavnik;
using StudentsConsultations.Models.Nastavnik.Prijavljivanje;
using StudentsConsultations.Models.Nastavnik.Registracija;
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

            CreateMap<Nastavnik, NastavnikDto>();

            CreateMap<NastavnikRequest, Nastavnik>();

            CreateMap<PrijavljivanjeRequest, Nastavnik>();

            CreateMap<RegistracijaRequest, Nastavnik>();
        }
    }
}
