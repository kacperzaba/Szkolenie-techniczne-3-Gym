using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Gym.Client.Storage.Entities;

namespace Gym.Client.Storage
{
    public class ClientDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ClientDbContext(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerSubscription> CustomerSubscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=client-db;trusted_connection=true;",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Client"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
