using gk_system_api.Models;
using Microsoft.EntityFrameworkCore;

namespace gk_system_api.Entities
{
    public class GkSystemDbContext: DbContext
    {
        //private const string CONNECTION_STRING = "Server=(LocalDb)\\MSSQLLocalDB;Database=GkSystem;Trusted_Connection=True";
        private const string CONNECTION_STRING = "Server=mssql3.webio.pl,2401;Database=jpruszydlo1_gksystem;user id=jpruszydlo1_shortener_admin;Password=JakPru112023(;TrustServerCertificate=True;Integrated security=false";

        public DbSet<GeneralConfig> GeneralConfig { get; set; }
        public DbSet<CarouselConfig> CarouselConfig { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferPlan> OfferPlans { get; set; }
        public DbSet<OfferPlanParams> OfferPlanParams { get; set; }
        public DbSet<OfferParams> OfferParams { get; set; }
        public DbSet<OfferVisualisations> OfferVisualisations { get; set; }
        public DbSet<Realisation> Realisations { get; set; }
        public DbSet<RealisationImage> RealisationImages { get; set; }
        public DbSet<Reference> References { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<Visitor> Visitors { get; set; }
        public DbSet<Localisation> Localisations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                CONNECTION_STRING,
                options => options.EnableRetryOnFailure()
            );
        }

    }
}
