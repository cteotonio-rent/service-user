using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using rent.user.infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Testcontainers.MongoDb;

namespace WebApi.Test
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        private MongoDbContainer mongoContainer;
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

                  mongoContainer = new MongoDbBuilder()
                    .WithImage("mongo:latest")
                    .Build();

                  var cancellationTokenSource = new CancellationTokenSource();
                  cancellationTokenSource.CancelAfter(TimeSpan.FromMinutes(5));

                  mongoContainer.StartAsync(cancellationTokenSource.Token).GetAwaiter().GetResult(); // Inicia o contêiner de forma síncrona

                  var mongoClient = new MongoClient(mongoContainer.GetConnectionString());

                  services.AddDbContext<UserDbContext>(options =>
                  {
                      options.UseMongoDB(mongoClient, "db_test");
                      //options.UseInternalServiceProvider(services.BuildServiceProvider());
                  });
              });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                mongoContainer.DisposeAsync().GetAwaiter().GetResult();
            }

            base.Dispose(disposing);
        }
    }
}
