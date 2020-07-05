using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Laci.Models;
using Laci.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Npgsql.TypeHandlers.GeometricHandlers;

namespace Laci.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly CityService _cityService;
        private readonly RecordService _recordService;

        private readonly ILogger<AdminController> _logger;

        private readonly Dictionary<string, string> _columnMappings = new Dictionary<string, string>{
            { "\"geo_merge\"", "City" },
            { "\"population\"", "Population" },
            { "\"persons_tested_final\"", "Tests" },
            { "\"cases_final\"", "Cases" },
            { "\"deaths_final\"", "Deaths" }
        };

        public AdminController(CityService cityService, RecordService recordService,
            ILogger<AdminController> logger)
        {
            _cityService = cityService;
            _recordService = recordService;

            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateTime date, IFormFile file)
        {
            using var reader = new StreamReader(file.OpenReadStream());

            string line = reader.ReadLine();
            string[] words = line.ToLower().Split(',');
            Dictionary<string, int> columnIndexes = new Dictionary<string, int>();
            for (var i = 0; i < words.Length; ++i)
                if (_columnMappings.ContainsKey(words[i]))
                    columnIndexes.Add(_columnMappings[words[i]], i);

            if (!columnIndexes.ContainsKey("City"))
            {
                _logger.LogWarning("No city column found in the uploaded file");
                return RedirectToAction("Index");
            }

            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrEmpty(line)) continue;

                words = line.Split(',');
                var name = words[columnIndexes["City"]];
                name = name.Substring(1, name.Length - 2); // strip the quotes
                var city = _cityService.GetCity(name);
                if (city == null)
                {
                    city = new City { Name = name };
                    _cityService.AddCity(city);
                    _cityService.SaveChanges();
                }

                if (columnIndexes.ContainsKey("Population"))
                    city.Population = int.Parse(words[columnIndexes["Population"]]);

                var record = _recordService.GetRecord(city.Id, date);
                if (record == null)
                {
                    record = new Record
                    {
                        CityId = city.Id,
                        Date = date
                    };
                    _recordService.AddRecord(record);
                }

                if (columnIndexes.ContainsKey("Tests"))
                    record.Tests = int.Parse(words[columnIndexes["Tests"]]);
                if (columnIndexes.ContainsKey("Cases"))
                    record.Cases = int.Parse(words[columnIndexes["Cases"]]);
                if (columnIndexes.ContainsKey("Deaths"))
                    record.Deaths = int.Parse(words[columnIndexes["Deaths"]]);
            }
            _cityService.SaveChanges();
            _recordService.SaveChanges();

            _logger.LogInformation("{username} uploaded {file} for {date}",
                User.FindFirst(ClaimTypes.NameIdentifier)?.Value,
                file.FileName, date.Date);
            return RedirectToAction("Index");
        }
    }
}
