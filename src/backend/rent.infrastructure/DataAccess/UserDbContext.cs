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
        public DbSet<Rental> Rentals { get; set; }

        public DbSet<RentalPlan> RentalPlans { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryPersonNotification> DeliveryPersonNotifications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserDbContext).Assembly);
            modelBuilder.Entity<User>().ToCollection("user");
            modelBuilder.Entity<Motorcycle>().ToCollection("motorcycle");
            modelBuilder.Entity<Rental>().ToCollection("rental");
            modelBuilder.Entity<RentalPlan>().ToCollection("rentalplan");
            modelBuilder.Entity<Order>().ToCollection("order");
            modelBuilder.Entity<DeliveryPersonNotification>().ToCollection("deliverypersonnotification");
        }
    }
}
