using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Student;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    [Route("studenti")]
    public class StudentController : Controller
    {
        private IStudentService _iStudentService;
        private IMapper _mapper;

        public StudentController(IStudentService iStudentService, IMapper mapper)
        {
            _iStudentService = iStudentService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var studenti = _iStudentService.GetAll();
            var studentiRowDto = _mapper.Map<IEnumerable<StudentRowDto>>(studenti);

            return Ok(studentiRowDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = _iStudentService.GetById(id);
            var studentDto = _mapper.Map<StudentDto>(student);

            return Ok(studentDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]StudentRequest request)
        {
            var student = _mapper.Map<Student>(request);
            _iStudentService.Insert(student);

            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody]StudentRequest request)
        {
            var studentRequest = _iStudentService.GetById(id);

            if (studentRequest == null)
            {
                return NotFound("Student ne postoj!.");
            }

            studentRequest.Ime = request.Ime;
            studentRequest.Prezime = request.Prezime;
            studentRequest.BrojIndeksa = request.BrojIndeksa;

            var student = _mapper.Map<Student>(studentRequest);
            _iStudentService.Update(student);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = _iStudentService.GetById(id);

            if (student == null)
            {
                return NotFound("Student ne postoj!.");
            }

            _iStudentService.Delete(student);

            return new NoContentResult();
        }
    }
}