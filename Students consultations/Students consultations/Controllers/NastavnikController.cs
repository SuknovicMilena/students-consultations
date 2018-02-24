using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Nastavnik;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    [Route("nastavnici")]
    public class NastavnikController : Controller
    {
        private INastavnikService _iNastavnikService;
        private IMapper _mapper;

        public NastavnikController(INastavnikService iNastavnikService, IMapper mapper)
        {
            _iNastavnikService = iNastavnikService;
            _mapper = mapper;
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