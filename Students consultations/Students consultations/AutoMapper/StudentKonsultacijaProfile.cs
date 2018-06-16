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
                //.AfterMap((src, dest) =>
            //{
                //var vremeOd = TimeSpan.Parse(src.VremeOd);
                //dest.VremeOd = DateTime.Today.Add(vremeOd).ToUniversalTime();

                //var vremeDo = TimeSpan.Parse(src.VremeDo);
                //dest.VremeDo = DateTime.Today.Add(vremeDo).ToUniversalTime();
            //}
        //);

            CreateMap<StudentKonsultacija, StudentKonsultacijaRowDto>();
        }
    }
}
