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


        //[HttpPost("getkonsultacija/{studentId}/{nastavnikId}")]
        //public IActionResult GetKonsultacija(int studentId, int nastavnikId, [FromBody]DatumKonsultacijaRequest request)
        //{
        //    DateTime datumTicks = new DateTime(datumKonsultacija);
        //    String datum = datumTicks.ToString("yyyy-MM-dd");

        //    var konsultacija = _iKonsultacijeService.GetKonsultacija(studentId, nastavnikId, DateTime.Parse(request.DatumString));

        //    return Ok(_mapper.Map<StudentKonsultacijaRowDto>(konsultacija));
        //}

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

            request.DatumKonsultacija = DateTime.Parse(request.DatumString);

            konsultacije.RazlogId = razlogId;
            _iKonsultacijeService.Insert(konsultacije, request.DatumKonsultacija);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]StudentKonsultacijaRequest request)
        {
            var razlogZaUpdate = _mapper.Map<Razlog>(request.Razlog);
            _iRazlogService.Update(razlogZaUpdate);

            var konsultacijaIzBaze = _iKonsultacijeService.GetKonsultacija(request.StudentId, request.NastavnikId, request.DatumKonsultacija);

            if (konsultacijaIzBaze == null)
            {
                return new NotFoundResult();
            }

            konsultacijaIzBaze.Nastavnik.Id = request.NastavnikId;
            konsultacijaIzBaze.Student.Id = request.StudentId;
            konsultacijaIzBaze.Odrzane = request.Odrzane;

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
            var konsultacija = _mapper.Map<StudentKonsultacija>(request);

            _iKonsultacijeService.Delete(konsultacija);

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