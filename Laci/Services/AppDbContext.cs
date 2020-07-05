using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Laci.Models;
using Microsoft.EntityFrameworkCore;

namespace Laci.Services
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Record> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasIndex(r => r.Name).IsUnique();
            modelBuilder.Entity<Record>().HasKey(r => new { r.CityId, r.Date });
        }
    }
}
