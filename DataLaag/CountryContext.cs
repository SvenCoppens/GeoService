using DataLaag.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLaag
{
    public class CountryContext : DbContext
    {
        private string ConnectionString;
        public CountryContext(string db="Production") : base()
        {
            ConfigureConnectionString(db);
        }
        private void ConfigureConnectionString(string db)
        {
            switch (db)
            {
                case "Production":
                    ConnectionString = @"Data Source=DESKTOP-VCI7746\SQLEXPRESS;Initial Catalog=GeoService;Integrated Security=True";
                    Database.EnsureCreated();
                    break;
                case "Test":
                    ConnectionString = @"Data Source=DESKTOP-VCI7746\SQLEXPRESS;Initial Catalog=GeoServiceTests;Integrated Security=True";
                    Database.EnsureDeleted();
                    Database.EnsureCreated();
                    break;
            }
        }
        public DbSet<DataCity> Cities { get; set; }
        public DbSet<DataContinent> Continents { get; set; }
        public DbSet<DataCountry> Countries { get; set; }
        public DbSet<DataRiver> Rivers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataCountryRiver>().HasKey(x => new { x.CountryId, x.RiverId });

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}
