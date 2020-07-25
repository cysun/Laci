using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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

        private readonly IMapper _mapper;

        public ApiController(CityService cityService, RecordService recordService, IMapper mapper)
        {
            _cityService = cityService;
            _recordService = recordService;
            _mapper = mapper;
        }

        [HttpGet("api/cities")]
        public List<City> Cites()
        {
            return _cityService.GetCities();
        }

        [HttpGet("api/cities/{cityId}/records")]
        public List<RecordViewModel> CityRecords(int cityId)
        {
            var records = _recordService.GetRecords(cityId)
                .Select(r => _mapper.Map<RecordViewModel>(r)).ToList();

            records[0].NewCases = 0;
            for (int i = 1; i < records.Count; ++i)
            {
                records[i].NewCases = (records[i].Cases - records[i - 1].Cases) /
                    (records[i].Date - records[i - 1].Date).Days;
            }

            return records;
        }
    }
}
