using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Nastavnik;
using StudentsConsultations.Models.Nastavnik.Prijavljivanje;
using StudentsConsultations.Models.Nastavnik.Registracija;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    //[Authorize]
    [Route("nastavnici")]
    public class NastavnikController : Controller
    {
        private INastavnikService _iNastavnikService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public NastavnikController(INastavnikService iNastavnikService, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _iNastavnikService = iNastavnikService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }

        [AllowAnonymous]
        [HttpPost("prijavljivanje")]
        public IActionResult Authenticate([FromBody]PrijavljivanjeRequest request)
        {
            var nastavnik = _iNastavnikService.Authenticate(request.KorisnickoIme, request.Lozinka);

            if (nastavnik == null)
                return Unauthorized();

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
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = nastavnik.Id,
                FirstName = nastavnik.Ime,
                LastName = nastavnik.Prezime,
                Username = nastavnik.KorisnickoIme,
                Token = tokenString
            });
        }

        [AllowAnonymous]
        [HttpPost("registracija")]
        public IActionResult Register([FromBody]RegistracijaRequest request)
        {
            var nastavnik = _mapper.Map<Nastavnik>(request);

            if (string.IsNullOrWhiteSpace(nastavnik.Lozinka))
                return new BadRequestObjectResult("Lozinka je obavezna!");

            if (_iNastavnikService.GetAll().Any(x => x.KorisnickoIme == request.KorisnickoIme))
                return new BadRequestObjectResult("Korisnicko ime " + request.KorisnickoIme + " vec postoji!");

            _iNastavnikService.Insert(nastavnik);
            return Ok();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var nastavnici = _iNastavnikService.GetAll();
            var nastavniciRowDto = _mapper.Map<IEnumerable<NastavnikRowDto>>(nastavnici);

            return Ok(nastavniciRowDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var nastavnik = _iNastavnikService.GetById(id);
            var nastavnikDto = _mapper.Map<NastavnikDto>(nastavnik);

            return Ok(nastavnikDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]NastavnikRequest request)
        {
            var nastavnik = _mapper.Map<Nastavnik>(request);
            _iNastavnikService.Insert(nastavnik);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]NastavnikRequest request)
        {
            var nastavnikRequest = _iNastavnikService.GetById(request.Id);

            if (nastavnikRequest == null)
            {
                return NotFound("Nastavnik ne postoj!.");
            }

            _mapper.Map(request, nastavnikRequest);

            _iNastavnikService.Update(nastavnikRequest);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var nastavnik = _iNastavnikService.GetById(id);

            if (nastavnik == null)
            {
                return NotFound("Nastavnik ne postoj!.");
            }

            _iNastavnikService.Delete(nastavnik);

            return new NoContentResult();
        }
    }
}