using Gym.FitnessClass.Storage.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Gym.FitnessClass.Storage
{
    public class FitnessClassDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public FitnessClassDbContext(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
        }

        public DbSet<Storage.Entities.FitnessClass> FitnessClasses { get; set; }
        public DbSet<FitnessClassCustomer> FitnessClassClients { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=fitnessclass-db;trusted_connection=true;",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "FitnessClass"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
