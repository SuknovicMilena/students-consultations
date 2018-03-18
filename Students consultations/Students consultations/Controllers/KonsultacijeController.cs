﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Data.Domain;
using StudentsConsultations.Models.Konsultacije;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    [Route("konsultacije")]
    public class KonsultacijeController : Controller
    {
        private IKonsultacijeService _iKonsultacijeService;
        private IDatumService _iDatumService;
        private IMapper _mapper;

        public KonsultacijeController(IKonsultacijeService iKonsultacijeService, IDatumService iDatumService, IMapper mapper)
        {
            _iKonsultacijeService = iKonsultacijeService;
            _iDatumService = iDatumService;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Create([FromBody]KonsultacijeRequest request)
        {
            var konsultacije = _mapper.Map<Konsultacije>(request);
            _iKonsultacijeService.Insert(konsultacije);

            return Ok();
        }

    }
}