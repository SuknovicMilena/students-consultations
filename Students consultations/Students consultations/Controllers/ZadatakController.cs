using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Zadatak;
using StudentsConsultations.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentsConsultations.Controllers
{
    [Route("zadaci")]
    public class ZadatakController : Controller
    {
        private IZadatakService _iZadatakService;
        private IMapper _mapper;

        public ZadatakController(IZadatakService iZadatakService, IMapper mapper)
        {
            _iZadatakService = iZadatakService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody]ZadatakRequest request)
        {
            var zadatak = _mapper.Map<Zadatak>(request);
            _iZadatakService.Insert(zadatak);

            return Ok();
        }
        // test
    }
}
