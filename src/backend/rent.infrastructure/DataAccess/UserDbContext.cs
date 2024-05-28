using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using rent.domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace rent.infrastructure.DataAccess
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Motorcycle> Motorcycles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
            modelBuilder.Entity<User>().ToCollection("user");
            modelBuilder.Entity<Motorcycle>().ToCollection("motorcycle");
        }
    }
}
