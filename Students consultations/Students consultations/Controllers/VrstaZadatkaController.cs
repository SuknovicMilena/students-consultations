using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.VrstaZadatka;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Controllers
{
    public class VrstaZadatkaController : Controller
    {
        private IVrstaZadatkaService _iVrstaZadatka;
        private IMapper _mapper;

        public VrstaZadatkaController(IVrstaZadatkaService iVrstaZadatka, IMapper mapper)
        {
            _iVrstaZadatka = iVrstaZadatka;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var vrsteZadatka = _iVrstaZadatka.GetAll();
            var vrsteZadatkaRowDto = _mapper.Map<IEnumerable<VrstaZadatkaRowDto>>(vrsteZadatka);

            return Ok(vrsteZadatkaRowDto);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var vrstaZadatka = _iVrstaZadatka.GetById(id);
            var vrstaZadatkaDto = _mapper.Map<VrstaZadatkaDto>(vrstaZadatka);

            return Ok(vrstaZadatkaDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody]VrstaZadatkaRequest request)
        {
            var vrstaZadatka = _mapper.Map<VrstaZadatka>(request);
            _iVrstaZadatka.Insert(vrstaZadatka);

            return Ok();
        }

        [HttpPut]
        public IActionResult Update([FromBody]VrstaZadatka request)
        {
            var vrstaZadatkaRequest = _iVrstaZadatka.GetById(request.Id);

            if (vrstaZadatkaRequest == null)
            {
                return NotFound("Vrsta zadatka ne postoj!.");
            }

            _mapper.Map(request, vrstaZadatkaRequest);

            _iVrstaZadatka.Update(vrstaZadatkaRequest);

            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vrstaZadatka = _iVrstaZadatka.GetById(id);

            if (vrstaZadatka == null)
            {
                return NotFound("Vrsta zadatka ne postoj!.");
            }

            _iVrstaZadatka.Delete(vrstaZadatka);

            return new NoContentResult();
        }
    }

}
}
