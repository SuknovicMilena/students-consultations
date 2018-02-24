using AutoMapper;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Student;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.AutoMapper
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            CreateMap<Student, StudentDto>();

            CreateMap<Student, StudentRowDto>();

            CreateMap<StudentRequest, Student>();
        }
    }
}
