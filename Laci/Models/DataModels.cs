using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Laci.Models
{
    public class City
    {
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Name { get; set; }
        public int Population { get; set; }
    }

    public class Record
    {
        public int CityId { get; set; }
        public City City { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public int? Tests { get; set; }
        public int? Cases { get; set; }
        public int? Deaths { get; set; }
    }
}
