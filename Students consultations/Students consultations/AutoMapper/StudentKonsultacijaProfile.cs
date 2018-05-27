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
    public class StudentKonsultacijaProfile : Profile
    {
        public StudentKonsultacijaProfile()
        {
            CreateMap<StudentKonsultacijaRequest, StudentKonsultacija>();

            CreateMap<StudentKonsultacija, StudentKonsultacijaRowDto>();
        }
    }
}
