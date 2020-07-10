using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Laci.Models;
using Laci.Services;

namespace Laci.Controllers
{
    public class HomeController : Controller
    {
        private readonly CityService _cityService;
        private readonly RecordService _recordService;

        public HomeController(CityService cityService, RecordService recordService)
        {
            _cityService = cityService;
            _recordService = recordService;
        }

        public IActionResult Index()
        {
            return View(_cityService.GetCities());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
