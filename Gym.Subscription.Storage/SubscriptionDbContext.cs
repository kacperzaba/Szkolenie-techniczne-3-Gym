using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Subscription.Storage
{
    public class SubscriptionDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public SubscriptionDbContext(IConfiguration configuration)
            : base()
        {
            _configuration = configuration;
        }

        public DbSet<Entities.Subscription> Subscriptions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"server=(localdb)\mssqllocaldb;database=subscription-db;trusted_connection=true;",
                x => x.MigrationsHistoryTable("__EFMigrationsHistory", "Subscription"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
