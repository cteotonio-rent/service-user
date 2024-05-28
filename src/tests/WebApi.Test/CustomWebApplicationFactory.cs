using CommomTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using rent.domain.Entities;
using rent.infrastructure.DataAccess;
using Testcontainers.MongoDb;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly MongoDbContainer mongoContainer;
        private readonly MongoClient mongoClient;

        private rent.domain.Entities.User _user = default!;
        private rent.domain.Entities.Motorcycle _motorcycle = default!;
        private string _password = string.Empty;

        public CustomWebApplicationFactory()
        {
            mongoContainer = new MongoDbBuilder()
                .WithImage("mongo:latest")
                .Build();

            var cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(5));

            mongoContainer.StartAsync(cancellationTokenSource.Token).Wait(); // Inicia o contêiner de forma síncrona

            mongoClient = new MongoClient(mongoContainer.GetConnectionString());
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing")
             .ConfigureServices(services =>
             {
                 var descriptor = services.SingleOrDefault(
                 d => d.ServiceType ==
                     typeof(DbContextOptions<UserDbContext>));

                 if (descriptor != null)
                     services.Remove(descriptor);

                 services.AddDbContext<UserDbContext>(options =>
                 {
                     options.UseMongoDB(mongoClient, "db_test");
                 });

                 using var scope = services.BuildServiceProvider().CreateScope();

                 var dbContext = scope.ServiceProvider.GetRequiredService<UserDbContext>();

                 StartDatabase(dbContext);
             });


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (mongoContainer != null)
                    mongoContainer.DisposeAsync().AsTask().Wait();
            }

            base.Dispose(disposing);
        }

        public string GetEmail() => _user.Email;
        public string GetPassword() => _password;
        public string GetName() => _user.Name;
        public rent.domain.Entities.Motorcycle GetMotorcycle() => _motorcycle;

        public Guid GetUserIdentifier() => _user.UserUniqueIdentifier;

        private void StartDatabase(UserDbContext dbContext)
        {
            (_user, _password) = UserBuilder.Build();
            dbContext.Users.Add(_user);

            _motorcycle = MotorcycleBuilder.Build();
            _motorcycle.UserUniqueIdentifier = _user.UserUniqueIdentifier;
            dbContext.Motorcycles.Add(_motorcycle);

            dbContext.SaveChanges();
        }
    }

}
