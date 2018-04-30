using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Konsultacije;
using StudentsConsultations.Models.Projekat;
using StudentsConsultations.Models.Razlog;
using StudentsConsultations.Models.Search;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    [Route("konsultacije")]
    public class KonsultacijeController : Controller
    {
        private IKonsultacijeService _iKonsultacijeService;
        private IDatumService _iDatumService;
        private IRazlogService _iRazlogService;
        private IIspitService _iIspitService;
        private IZavrsniRadService _iZavrsniRadService;
        private IProjekatService _iProjekatService;
        private IMapper _mapper;

        public KonsultacijeController(IKonsultacijeService iKonsultacijeService, IIspitService iIspitService, IZavrsniRadService iZavrsniRadService,
        IProjekatService iProjekatService, IRazlogService iRazlogService, IDatumService iDatumService, IMapper mapper)
        {
            _iKonsultacijeService = iKonsultacijeService;
            _iDatumService = iDatumService;
            _iRazlogService = iRazlogService;
            _iIspitService = iIspitService;
            _iProjekatService = iProjekatService;
            _iZavrsniRadService = iZavrsniRadService;
            _mapper = mapper;
        }

        [HttpGet("bystudent/{studentId}")]
        public IActionResult GetAllByStudent(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GetAllKonsultacijeByStudentId(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("bynastavnik/{nastavnikId}")]
        public IActionResult GetAllByNastavnik(int nastavnikId)
        {
            var konsultacije = _iKonsultacijeService.GetAllKonsultacijeByNastavnik(nastavnikId);

            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("groupbynastavnik/{studentId}")]
        public IActionResult GroupKonsultacijeByNastavnik(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GroupKonsultacijeByNastavnikForStudent(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("groupbydatum/{studentId}")]
        public IActionResult GroupKonsultacijeByDatum(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GroupKonsultacijeByDatumForStudent(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody]KonsultacijeRequest request)
        {
            var razlogZaBazu = _mapper.Map<Razlog>(request.Razlog);
            var razlogId = _iRazlogService.InsertAndReturnId(razlogZaBazu);

            if (request.Razlog.Type == RazlogType.Ispit)
            {
                var ispitZaBazu = new Ispit();

                ispitZaBazu.Naziv = request.Razlog.NazivTipa;
                ispitZaBazu.RazlogId = razlogId;

                _iIspitService.Insert(ispitZaBazu);
            }

            if (request.Razlog.Type == RazlogType.Projekat)
            {
                var projekatZaBazu = new Projekat();

                projekatZaBazu.NazivIspita = request.Razlog.NazivIspita;
                projekatZaBazu.NazivProjekta = request.Razlog.NazivTipa;
                projekatZaBazu.RazlogId = razlogId;

                _iProjekatService.Insert(projekatZaBazu);
            }

            if (request.Razlog.Type == RazlogType.ZavrsniRad)
            {
                var zavrsniRadZaBazu = new ZavrsniRad();

                zavrsniRadZaBazu.NazivZavrsnogRada = request.Razlog.NazivTipa;
                zavrsniRadZaBazu.Tip = request.Razlog.TipZavrsnogRada;
                zavrsniRadZaBazu.RazlogId = razlogId;

                _iZavrsniRadService.Insert(zavrsniRadZaBazu);
            }

            var konsultacije = _mapper.Map<Konsultacije>(request);

            konsultacije.RazlogId = razlogId;
            _iKonsultacijeService.Insert(konsultacije, request.DatumKonsultacija);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]KonsultacijeRequest request)
        {
            var konsultacije = _mapper.Map<Konsultacije>(request);
            _iKonsultacijeService.Update(konsultacije);

            return Ok();
        }


        [HttpPost("pretragaponastavniku/{studentId}")]
        public IActionResult SearchByNastavnik([FromBody]SearchRequest request, int studentId)
        {
            var konsultacije = _iKonsultacijeService.SearchByNastavnik(request.SearchText, studentId);
            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }
    }
}