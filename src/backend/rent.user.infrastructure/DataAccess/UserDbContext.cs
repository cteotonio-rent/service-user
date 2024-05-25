using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using rent.user.domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace rent.user.infrastructure.DataAccess
{
    public class UserDbContext: DbContext
    {
        public UserDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
            modelBuilder.Entity<User>().ToCollection("user");
        }
    }
}
