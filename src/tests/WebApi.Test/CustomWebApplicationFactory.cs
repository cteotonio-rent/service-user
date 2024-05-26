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
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");
             //.ConfigureServices(async services =>
             //{
             //    var descriptor = services.SingleOrDefault(
             //        d => d.ServiceType ==
             //            typeof(DbContextOptions<UserDbContext>));

             //    if (descriptor != null)
             //        services.Remove(descriptor);

             //    var mongoContainer = new MongoDbBuilder()
             //           .WithImage("mongo:6.0")
             //           .Build();

             //    await mongoContainer.StartAsync();

             //    //var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();
             //    var provider = services.AddEntityFrameworkMongoDB().BuildServiceProvider();


             //    var mongoClient = new MongoClient(mongoContainer.GetConnectionString());
             //    //services.AddDbContext<UserDbContext>(dbContextOptions => { dbContextOptions.UseMongoDB(mongoClient, databaseName: configuration.GetConnectionString("Database")!); });

             //    services.AddDbContext<UserDbContext>(options =>
             //    {
             //        options.UseMongoDB(mongoClient,"db_test");
                 
             //        //options.UseInMemoryDatabase("InMemoryDbForTesting");
             //        options.UseInternalServiceProvider(provider);
             //    });
             //});
        }
    }
}
