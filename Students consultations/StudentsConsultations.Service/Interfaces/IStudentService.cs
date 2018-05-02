using StudentsConsultations.Data.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service.Interfaces
{
    public interface IStudentService : IBaseService<Student>
    {
        Student Authenticate(string korisnickoIme, string lozinka);
    }
}
