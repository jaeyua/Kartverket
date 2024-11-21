using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Nettside.Models;
using System.Collections.Generic;

namespace Nettside.Data
{
    // DbContext-klasse for applikasjonens database som arver fra IdentityDbContext for bruk av brukerautentisering.
    public class AppDbContext : IdentityDbContext<Users>
    {
        // Konstruktør som setter opp databasens konfigurasjon ved å bruke de gitte alternativene.
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet for GeoChanges-tabellen som inneholder geodata for endringer.
        public DbSet<GeoChanges> GeoChange { get; set; }

        // DbSet for AreaChanges-tabellen som holder oversikt over de ulike områdeendringene.
        public DbSet<AreaChange> AreaChanges { get; set; }
    }
}
