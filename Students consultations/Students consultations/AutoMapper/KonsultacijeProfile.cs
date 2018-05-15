using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Konsultacije;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class KonsultacijeProfile : Profile
    {
        public KonsultacijeProfile()
        {
            CreateMap<KonsultacijeRequest, Konsultacije>();

            CreateMap<Konsultacije, KonsultacijeRowDto>();

            CreateMap<Datum, DatumKonsultacijaDto>();
        }
    }
}
