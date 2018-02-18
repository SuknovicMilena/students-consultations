using StudentsConsultations.Data.Domain;
using StudentsConsultations.Data.Interface;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudentsConsultations.Service
{
    public class StudentService : IStudentService
    {
        private readonly IDatabaseManager _databaseManager;

        public StudentService(IDatabaseManager databaseManager)
        {
            _databaseManager = databaseManager;
        }

        public IEnumerable<Student> GetAll()
        {
            return _databaseManager.StudentRepository.GetAll();
        }

        public Student GetById(int id)
        {
            return _databaseManager.StudentRepository.GetById(id);
        }

        public void Insert(Student student)
        {
            _databaseManager.StudentRepository.Insert(student);
            _databaseManager.SaveChanges();
        }

        public void Update(Student student)
        {
            _databaseManager.StudentRepository.Update(student);
            _databaseManager.SaveChanges();
        }

        public void Delete(Student student)
        {
            _databaseManager.StudentRepository.Delete(student);
            _databaseManager.SaveChanges();
        }
    }
}
