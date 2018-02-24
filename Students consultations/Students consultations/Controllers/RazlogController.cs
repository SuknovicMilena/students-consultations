using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentsConsultations.Service.Interfaces;

namespace StudentsConsultations.Controllers
{
    public class RazlogController : Controller
    {
        private IRazlogService _iRazlogService;
        private IMapper _mapper;

        public RazlogController(IRazlogService iRazlogService, IMapper mapper)
        {
            _iRazlogService = iRazlogService;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}