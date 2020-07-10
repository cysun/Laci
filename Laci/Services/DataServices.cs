using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Threading.Tasks;
using Laci.Models;
using Microsoft.EntityFrameworkCore;

namespace Laci.Services
{
    public class CityService
    {
        private readonly AppDbContext _db;

        public CityService(AppDbContext db)
        {
            _db = db;
        }

        public List<City> GetCities()
        {
            return _db.Cities.OrderBy(c => c.Name).ToList();
        }

        public City GetCity(string name)
        {
            return _db.Cities.Where(c => c.Name.ToUpper() == name.ToUpper()).SingleOrDefault();
        }

        public void AddCity(City city) => _db.Cities.Add(city);

        public void SaveChanges() => _db.SaveChanges();
    }

    public class RecordService
    {
        private readonly AppDbContext _db;

        public RecordService(AppDbContext db)
        {
            _db = db;
        }

        public Record GetRecord(int cityId, DateTime date)
        {
            return _db.Records.Where(r => r.CityId == cityId && r.Date.Date == date.Date)
                .SingleOrDefault();
        }

        public List<Record> GetRecords(int cityId)
        {
            return _db.Records.Where(r => r.CityId == cityId).OrderBy(r => r.Date).ToList();
        }

        public void AddRecord(Record record) => _db.Records.Add(record);

        public void SaveChanges() => _db.SaveChanges();
    }
}
