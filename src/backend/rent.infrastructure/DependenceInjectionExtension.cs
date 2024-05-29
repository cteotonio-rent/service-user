using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using rent.domain.Repositories;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Repositories.Rental;
using rent.domain.Repositories.RentalPlan;
using rent.domain.Repositories.User;
using rent.domain.Security.Tokens;
using rent.domain.Services.LoggedUser;
using rent.infrastructure.DataAccess;
using rent.infrastructure.Repositories;
using rent.infrastructure.Security.Access.Generator;
using rent.infrastructure.Security.Access.Validator;
using rent.infrastructure.Services.LoggedUser;

namespace rent.infrastructure
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
            services.AddDbContext<UserDbContext>(dbContextOptions => 
            { 
                dbContextOptions.UseMongoDB(
                    mongoClient, 
                    databaseName: configuration.GetConnectionString("Database")!); 
            });
        }

        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
            services.AddScoped<IUserReadOnlyRepository, UserRepository>();
            services.AddScoped<IUserUpdateOnlyRepository, UserRepository>();

            services.AddScoped<IMotorcycleWriteOnlyRepository, MotorcycleRepository>();
            services.AddScoped<IMotorcycleReadOnlyRepository, MotorcycleRepository>();
            services.AddScoped<IMotorcycleUpdateOnlyRepository, MotorcycleRepository>();

            services.AddScoped<IRentalReadOnlyRepository, RentalRepository>();
            services.AddScoped<IRentalWriteOnlyRepository, RentalRepository>();

            services.AddScoped<IRentalPlanReadOnlyRepository, RentalPlanRepository>();
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
