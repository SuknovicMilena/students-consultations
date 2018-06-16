using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Konsultacije;
using StudentsConsultations.Models.Projekat;
using StudentsConsultations.Models.Razlog;
using StudentsConsultations.Models.Search;
using StudentsConsultations.Service;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    [Route("student-konsultacije")]
    public class StudentKonsultacijaController : Controller
    {
        private IStudentKonsultacijaService _iKonsultacijeService;
        private IRazlogService _iRazlogService;
        private IIspitService _iIspitService;
        private IZavrsniRadService _iZavrsniRadService;
        private IProjekatService _iProjekatService;
        private IPDFGenerator _iPDFGenerator;
        private IMapper _mapper;

        public StudentKonsultacijaController(IStudentKonsultacijaService iKonsultacijeService, IIspitService iIspitService, IZavrsniRadService iZavrsniRadService,
        IProjekatService iProjekatService, IRazlogService iRazlogService, IPDFGenerator iPDFGenerator, IMapper mapper)
        {
            _iKonsultacijeService = iKonsultacijeService;
            _iRazlogService = iRazlogService;
            _iIspitService = iIspitService;
            _iProjekatService = iProjekatService;
            _iZavrsniRadService = iZavrsniRadService;
            _iPDFGenerator = iPDFGenerator;
            _mapper = mapper;
        }


        [HttpPost("getkonsultacija/{id}")]
        public IActionResult GetKonsultacija(int id)
        {
            var konsultacija = _iKonsultacijeService.GetKonsultacija(id);

            return Ok(_mapper.Map<StudentKonsultacijaRowDto>(konsultacija));
        }

        [HttpGet("bystudent/{studentId}")]
        public IActionResult GetAllByStudent(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GetAllKonsultacijeByStudentId(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<StudentKonsultacijaRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("bynastavnik/{nastavnikId}")]
        public IActionResult GetAllByNastavnik(int nastavnikId)
        {
            var konsultacije = _iKonsultacijeService.GetAllKonsultacijeByNastavnik(nastavnikId);

            var konsultacijeRowDtos = _mapper.Map<List<StudentKonsultacijaRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpPost("zakzanekonsultacijebynastavnik")]
        public IActionResult GetAllZakazaneKonsultacijeByNastavnik([FromBody] ZakazaneKonsultacijeRequest request)
        {
            var konsultacije = _iKonsultacijeService.GetAllZakazaneKonsultacijeKonsultacijeByNastavnik(request.NastavnikId, request.ZeljeniDatum);

            var konsultacijeRowDtos = _mapper.Map<List<ZakazaneKonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("groupbynastavnik/{studentId}")]
        public IActionResult GroupKonsultacijeByNastavnik(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GroupKonsultacijeByNastavnikForStudent(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<StudentKonsultacijaRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpGet("groupbydatum/{studentId}")]
        public IActionResult GroupKonsultacijeByDatum(int studentId)
        {
            var konsultacije = _iKonsultacijeService.GroupKonsultacijeByDatumForStudent(studentId);

            var konsultacijeRowDtos = _mapper.Map<List<StudentKonsultacijaRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody]StudentKonsultacijaRequest request)
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

            var konsultacije = _mapper.Map<StudentKonsultacija>(request);

            konsultacije.RazlogId = razlogId;
            _iKonsultacijeService.Insert(konsultacije);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]StudentKonsultacijaRequest request)
        {
            var razlogZaUpdate = _mapper.Map<Razlog>(request.Razlog);
            _iRazlogService.Update(razlogZaUpdate);

            var konsultacijaIzBaze = _iKonsultacijeService.GetKonsultacija(request.Id);

            if (konsultacijaIzBaze == null)
            {
                return new NotFoundResult();
            }

            // konsultacijaIzBaze.Nastavnik.Id = request.NastavnikId;
            konsultacijaIzBaze.Odrzane = request.Odrzane;
            konsultacijaIzBaze.VremeOd = request.VremeOd.ToUniversalTime();
            //DateTime.Parse(request.VremeOd).ToUniversalTime();
            konsultacijaIzBaze.VremeDo = request.VremeDo.ToUniversalTime();
            //DateTime.Parse(request.VremeDo).ToUniversalTime();

            _iKonsultacijeService.Update(konsultacijaIzBaze);

            return Ok();
        }

        [HttpPost("pretragaponastavniku/{studentId}")]
        public IActionResult SearchByNastavnik([FromBody]SearchRequest request, int studentId)
        {
            var konsultacije = _iKonsultacijeService.SearchByNastavnik(request.SearchText, studentId);
            var konsultacijeRowDtos = _mapper.Map<List<StudentKonsultacijaRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpPost("pretragapostudentu/{nastavnikId}")]
        public IActionResult SearchByStudent([FromBody]SearchRequest request, int nastavnikId)
        {
            var konsultacije = _iKonsultacijeService.SearchByStudent(request.SearchText, nastavnikId);
            var konsultacijeRowDtos = _mapper.Map<List<StudentKonsultacijaRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpPost("delete")]
        public IActionResult Delete([FromBody]StudentKonsultacijaRequest request)
        {
            var konsultacijaIzBaze = _iKonsultacijeService.GetKonsultacija(request.Id);

            if (konsultacijaIzBaze == null)
            {
                return new NotFoundResult();
            }

            _iKonsultacijeService.Delete(konsultacijaIzBaze);

            return Ok();
        }

        [HttpPost("generatepdfforstudent/{studentId}")]
        public IActionResult GeneratePDFForStudent([FromBody]SearchRequest request, int studentId)
        {
            var konsultacije = _iKonsultacijeService.GeneratePDFForStudent(request.SearchText, studentId);
            var pdf = _iPDFGenerator.GeneratePDF(konsultacije, UserType.Student);

            return File(pdf, "application/pdf", "konsultacije.pdf");
        }


        [HttpPost("generatepdffornastavnik/{nastavnikId}")]
        public IActionResult GeneratePDFForNastavnik([FromBody]SearchRequest request, int nastavnikId)
        {
            var konsultacije = _iKonsultacijeService.GeneratePDFForNastavnik(request.SearchText, nastavnikId);
            var pdf = _iPDFGenerator.GeneratePDF(konsultacije, UserType.Nastavnik);

            return File(pdf, "application/pdf", "konsultacije.pdf");
        }
    }
}