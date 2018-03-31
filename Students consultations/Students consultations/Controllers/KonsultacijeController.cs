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

        [HttpGet("{studentId}")]
        public IActionResult GetAll(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GetAllKonsultacijeByStudentId(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("groupbynastavnik/{studentId}")]
        public IActionResult GroupKonsultacijeByNastavnik(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GroupKonsultacijeByNastavnik(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("groupbydatum/{studentId}")]
        public IActionResult GroupKonsultacijeByDatum(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GroupKonsultacijeByDatum(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody]KonsultacijeRequest request)
        {
            var razlogZaBazu = _mapper.Map<Razlog>(request.razlog);
            var razlogId = _iRazlogService.InsertAndReturnId(razlogZaBazu);

            if (request.razlog.Type == RazlogType.Ispit)
            {
                var ispitZaBazu = new Ispit();

                ispitZaBazu.Naziv = request.razlog.NazivTipa;
                ispitZaBazu.RazlogId = razlogId;

                _iIspitService.Insert(ispitZaBazu);
            }

            if (request.razlog.Type == RazlogType.Projekat)
            {
                var projekatZaBazu = new Projekat();

                projekatZaBazu.NazivIspita = request.razlog.NazivIspita;
                projekatZaBazu.NazivProjekta = request.razlog.NazivTipa;
                projekatZaBazu.RazlogId = razlogId;

                _iProjekatService.Insert(projekatZaBazu);
            }

            if (request.razlog.Type == RazlogType.ZavrsniRad)
            {
                var zavrsniRadZaBazu = new ZavrsniRad();

                zavrsniRadZaBazu.NazivZavrsnogRada = request.razlog.NazivTipa;
                zavrsniRadZaBazu.Tip = request.razlog.TipZavrsnogRada;
                zavrsniRadZaBazu.RazlogId = razlogId;

                _iZavrsniRadService.Insert(zavrsniRadZaBazu);
            }

            var konsultacije = _mapper.Map<Konsultacije>(request);
            _iKonsultacijeService.Insert(konsultacije);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]KonsultacijeRequest request)
        {
            var konsultacije = _mapper.Map<Konsultacije>(request);
            _iKonsultacijeService.Update(konsultacije);

            return Ok();
        }

    }
}