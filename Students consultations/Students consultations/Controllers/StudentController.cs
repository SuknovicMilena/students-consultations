using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Helpers;
using StudentsConsultations.Models.Student;
using StudentsConsultations.Models.Student.Prijavljivanje;
using StudentsConsultations.Models.Student.Registracija;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    [Authorize]
    [Route("studenti")]
    public class StudentController : Controller
    {
        private IStudentService _iStudentService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public StudentController(IStudentService iStudentService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _iStudentService = iStudentService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("prijavljivanje")]
        public IActionResult Authenticate([FromBody]PrijavljivanjeRequest request)
        {
            var student = _iStudentService.Authenticate(request.KorisnickoIme, request.Lozinka);

            if (student == null)
                return Unauthorized();

            var verifyPassword = PasswordHelper.VerifyPassword(request.Lozinka, student.Lozinka);

            var tokenString = "";

            if (verifyPassword)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, request.KorisnickoIme.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                tokenString = tokenHandler.WriteToken(token);
            }

            return Ok(new
            {
                studentId = student.Id,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("registracija")]
        public IActionResult Register([FromBody]RegistracijaRequest request)
        {
            var createPassword = PasswordHelper.CreateHash(request.Lozinka);

            var student = _mapper.Map<Student>(request);

            student.Lozinka = createPassword;

            if (string.IsNullOrWhiteSpace(student.Lozinka))
                return new BadRequestObjectResult("Lozinka je obavezna!");

            if (_iStudentService.GetAll().Any(x => x.KorisnickoIme == request.KorisnickoIme))
                return new BadRequestObjectResult("Korisnicko ime " + request.KorisnickoIme + " vec postoji!");

            _iStudentService.Insert(student);
            return Ok();
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

        [HttpPut]
        public IActionResult Update([FromBody]StudentRequest request)
        {
            var studentRequest = _iStudentService.GetById(request.Id);

            if (studentRequest == null)
            {
                return NotFound("Student ne postoj!.");
            }

            _mapper.Map(request, studentRequest);
            _iStudentService.Update(studentRequest);

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