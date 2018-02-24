using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Razlog;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    [Route("razlozi")]
    public class RazlogController : Controller
    {
        private IRazlogService _iRazlogService;
        private IIspitService _iIspitService;
        private IZavrsniRadService _iZavrsniRadService;
        private IProjekatService _iProjekatService;
        private IMapper _mapper;

        public RazlogController(IRazlogService iRazlogService, IIspitService iIspitService, IZavrsniRadService iZavrsniRadService,
        IProjekatService iProjekatService, IMapper mapper)
        {
            _iRazlogService = iRazlogService;
            _iIspitService = iIspitService;
            _iProjekatService = iProjekatService;
            _iZavrsniRadService = iZavrsniRadService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var razlozi = _iRazlogService.GetAll();
            var razloziRowDto = _mapper.Map<IEnumerable<RazlogRowDto>>(razlozi);

            return Ok(razloziRowDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var razlog = _iRazlogService.GetById(id);
            var razlogDto = _mapper.Map<RazlogDto>(razlog);

            return Ok(razlogDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]RazlogRequest request)
        {
            var razlogZaBazu = _mapper.Map<Razlog>(request);
            var razlogId = _iRazlogService.InsertAndReturnId(razlogZaBazu);

            if (request.Type == RazlogType.Ispit)
            {
                var ispitZaBazu = new Ispit();

                ispitZaBazu.Naziv = request.NazivTipa;
                ispitZaBazu.RazlogId = razlogId;

                _iIspitService.Insert(ispitZaBazu);
            }

            if (request.Type == RazlogType.Projekat)
            {
                var projekatZaBazu = new Projekat();

                projekatZaBazu.NazivIspita = request.NazivIspita;
                projekatZaBazu.NazivProjekta = request.NazivTipa;
                projekatZaBazu.RazlogId = razlogId;

                _iProjekatService.Insert(projekatZaBazu);
            }

            if (request.Type == RazlogType.ZavrsniRad)
            {
                var zavrsniRadZaBazu = new ZavrsniRad();

                zavrsniRadZaBazu.NazivZavrsnogRada = request.NazivTipa;
                zavrsniRadZaBazu.Tip = request.TipZavrsnogRada;
                zavrsniRadZaBazu.RazlogId = razlogId;

                _iZavrsniRadService.Insert(zavrsniRadZaBazu);
            }

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]RazlogRequest request)
        {
            var razlog = _iRazlogService.GetById(request.RazlogId);

            if (razlog == null)
            {
                return NotFound("Razlog ne postoji.");
            }

            var razlogZaIzmenu = _mapper.Map(request, razlog);

            if (request.Type == RazlogType.Ispit)
            {
                var ispitZaIzmenu = new Ispit();

                ispitZaIzmenu.Naziv = request.NazivTipa;
                ispitZaIzmenu.RazlogId = razlogZaIzmenu.RazlogId;

                _iIspitService.Update(ispitZaIzmenu);
            }

            if (request.Type == RazlogType.Projekat)
            {
                var projekatZaIzmenu = new Projekat();

                projekatZaIzmenu.NazivIspita = request.NazivIspita;
                projekatZaIzmenu.NazivProjekta = request.NazivTipa;
                projekatZaIzmenu.RazlogId = razlogZaIzmenu.RazlogId;

                _iProjekatService.Update(projekatZaIzmenu);
            }

            if (request.Type == RazlogType.ZavrsniRad)
            {
                var zavrsniRadZaIzmenu = new ZavrsniRad();

                zavrsniRadZaIzmenu.NazivZavrsnogRada = request.NazivTipa;
                zavrsniRadZaIzmenu.Tip = request.TipZavrsnogRada;
                zavrsniRadZaIzmenu.RazlogId = razlogZaIzmenu.RazlogId;

                _iZavrsniRadService.Update(zavrsniRadZaIzmenu);
            }

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var razlog = _iRazlogService.GetById(id);

            if (razlog == null)
            {
                return NotFound("Razlog ne postoji.");
            }

            if (razlog.Projekat != null)
            {
                var projekat = _iProjekatService.GetById(razlog.RazlogId);

                _iProjekatService.Delete(projekat);
            }

            if (razlog.Ispit != null)
            {
                var ispit = _iIspitService.GetById(razlog.RazlogId);

                _iIspitService.Delete(ispit);
            }

            if (razlog.ZavrsniRad != null)
            {
                var zavrsniRad = _iZavrsniRadService.GetById(razlog.RazlogId);

                _iZavrsniRadService.Delete(zavrsniRad);
            }

            _iRazlogService.Delete(razlog);

            return new NoContentResult();
        }
    }
}