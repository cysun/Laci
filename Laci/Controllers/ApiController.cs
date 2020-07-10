using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laci.Models;
using Laci.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Laci.Controllers
{
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly CityService _cityService;
        private readonly RecordService _recordService;

        public ApiController(CityService cityService, RecordService recordService)
        {
            _cityService = cityService;
            _recordService = recordService;
        }

        [HttpGet("api/cities")]
        public List<City> Cites()
        {
            return _cityService.GetCities();
        }

        [HttpGet("api/cities/{cityId}/records")]
        public List<Record> CityRecords(int cityId)
        {
            return _recordService.GetRecords(cityId);
        }
    }
}
