using Microsoft.Extensions.DependencyInjection;
using rent.user.domain.Repositories.User;
using rent.user.infrastructure.Repositories;
using rent.user.infrastructure.DataAccess;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using rent.user.domain.Repositories;
using Microsoft.Extensions.Configuration;

namespace rent.user.infrastructure
{
    public static class DependenceInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
        }

        private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
        {
            var mongoClient = new MongoClient(configuration.GetConnectionString("Connection"));
            services.AddDbContext<UserDbContext>(dbContextOptions => { dbContextOptions.UseMongoDB(mongoClient, databaseName: configuration.GetConnectionString("Database")!); });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
        }
    }
}
