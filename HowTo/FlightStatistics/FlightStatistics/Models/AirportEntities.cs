using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightStatistics.Models
{
    public class AirportEntities : DbContext
    {
        public DbSet<Airport> Airports { get; set; }
        public DbSet<AirportMonthlyData> AirportsMonthlyData { get; set; }
        public AirportEntities(DbContextOptions<AirportEntities> options)
            : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(b =>
            {
                b.Property<string>("AirportCode");
                b.HasKey("AirportCode");
                b.HasAnnotation("Relational:TableName", "Airports");
            });

            modelBuilder.Entity<AirportMonthlyData>(b =>
            {
                b.Property<string>("AirportCode");
                b.HasKey("SNo");
                b.HasAnnotation("Relational:TableName", "AirportMonthlyData");
            });
        }
    }
}
