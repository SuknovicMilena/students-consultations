using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Konsultacije;
using StudentsConsultations.Models.Search;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Controllers
{
    [Route("konsultacije")]
    public class KonsultacijaController : Controller
    {
        private IKonsultacijaService _iKonsultacijeService;
        private IPDFGenerator _iPDFGenerator;
        private IMapper _mapper;

        public KonsultacijaController(IKonsultacijaService iKonsultacijeService, IPDFGenerator iPDFGenerator, IMapper mapper)
        {
            _iKonsultacijeService = iKonsultacijeService;
            _iPDFGenerator = iPDFGenerator;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public IActionResult GetKonsultacija(int id)
        {
            var konsultacija = _iKonsultacijeService.GetById(id);

            return Ok(_mapper.Map<KonsultacijaDto>(konsultacija));
        }

        [HttpGet("nastavnikId/{nastavnikId}/danUNedelji/{danUNedelji}")]
        public IActionResult GetKonsultacijaByNastavnikIdAndDanUNedelji(int nastavnikId, int danUNedelji)
        {
            var konsultacija = _iKonsultacijeService.GetByNastavnikIdAndDanUNedelji(nastavnikId, danUNedelji);

            return Ok(_mapper.Map<KonsultacijaDto>(konsultacija));
        }

        [HttpGet("getallbynastavnik/{nastavnikId}")]
        public IActionResult GetAll(int nastavnikId)
        {
            var konsultacije = _iKonsultacijeService.GetAllByNastavnik(nastavnikId);
            var konsultacijeRowDtos = _mapper.Map<List<KonsultacijeRowDto>>(konsultacije);

            return Ok(konsultacijeRowDtos);
        }

        [HttpPost]
        public IActionResult Create([FromBody]KonsultacijaRequest request)
        {
            var konsultacije = _mapper.Map<Konsultacija>(request);
            _iKonsultacijeService.Insert(konsultacije);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]KonsultacijaRequest request)
        {
            var konsultacijaIzBaze = _iKonsultacijeService.GetById(request.Id);

            if (konsultacijaIzBaze == null)
            {
                return new NotFoundResult();
            }

            konsultacijaIzBaze.DanUNedelji = request.DanUNedelji;
            konsultacijaIzBaze.VremeOd = DateTime.Parse(request.VremeOd).ToUniversalTime();
            konsultacijaIzBaze.VremeDo = DateTime.Parse(request.VremeDo).ToUniversalTime();

            _iKonsultacijeService.Update(konsultacijaIzBaze);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var konsultacijaIzBaze = _iKonsultacijeService.GetById(id);

            if (konsultacijaIzBaze == null)
            {
                return new NotFoundResult();
            }

            _iKonsultacijeService.Delete(konsultacijaIzBaze);

            return Ok();
        }
    }
}
