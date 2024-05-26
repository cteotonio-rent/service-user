using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using rent.user.infrastructure.DataAccess;
using Testcontainers.MongoDb;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly MongoDbContainer mongoContainer;
        private readonly MongoClient mongoClient;

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
    }

}
