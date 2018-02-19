using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models;
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
    }
}