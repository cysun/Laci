using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laci.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Population { get; set; }
    }

    public class Record
    {
        public int CityId { get; set; }
        public City City { get; set; }

        public DateTime Date { get; set; }

        public int Tests { get; set; }
        public int Cases { get; set; }
        public int Deaths { get; set; }
    }
}
