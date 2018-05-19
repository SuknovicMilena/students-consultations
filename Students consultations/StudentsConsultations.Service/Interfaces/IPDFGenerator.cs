using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Konsultacije;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service.Interfaces
{
    public interface IPDFGenerator
    {
        byte[] GeneratePDF(List<Konsultacije> konsultacije, UserType userType);
    }
}
