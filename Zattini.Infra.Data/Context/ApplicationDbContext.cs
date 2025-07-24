using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Zattini.Domain.Entities;

namespace Zattini.Infra.Data.Context
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
            Users = Set<User>();
            UserAddress = Set<UserAddress>();
        }

        public DbSet<User> Users { get; private set; }
        public DbSet<UserAddress> UserAddress { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
