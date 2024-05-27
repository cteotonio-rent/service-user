using Microsoft.Extensions.DependencyInjection;
using rent.user.domain.Repositories.User;
using rent.user.infrastructure.Repositories;
using rent.user.infrastructure.DataAccess;
using MongoDB.Driver;
using Microsoft.EntityFrameworkCore;
using rent.user.domain.Repositories;
using Microsoft.Extensions.Configuration;
using Testcontainers.MongoDb;
using DotNet.Testcontainers.Containers;
using rent.user.domain.Security.Tokens;
using rent.user.infrastructure.Security.Access.Generator;
using rent.user.infrastructure.Security.Access.Validator;
using rent.user.domain.Services.LoggedUser;
using rent.user.infrastructure.Services.LoggedUser;

namespace rent.user.infrastructure
{
    public static class DependenceInjectionExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            AddDbContext(services, configuration);
            AddRepositories(services);
            AddTokens(services, configuration);
            AddLoggedUser(services);
            if (configuration.GetValue<bool>("InMemoryTest"))
                return;
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
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();
        }

        private static void AddTokens(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccessTokenGenerator>(provider => new JwtTokenGenerator(
                configuration.GetValue<uint>("Jwt:ExpirationTimeMinutes"),
                singingKey: configuration.GetValue<string>("Jwt:SigningKey")!
            ));

            services.AddScoped<IAccessTokenValidator>(provider => new JwtTokenValidator(
                configuration.GetValue<string>("Jwt:SigningKey")!));
        }

        private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();

    }
}
