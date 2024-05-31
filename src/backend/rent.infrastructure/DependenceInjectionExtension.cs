using Amazon.S3;
using Amazon.SQS;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using rent.domain.Repositories;
using rent.domain.Repositories.Motorcycle;
using rent.domain.Repositories.NotifyDeliveryPerson;
using rent.domain.Repositories.Order;
using rent.domain.Repositories.Rental;
using rent.domain.Repositories.RentalPlan;
using rent.domain.Repositories.User;
using rent.domain.Security.Tokens;
using rent.domain.Services.LoggedUser;
using rent.domain.Services.ReadNewOrderMessage;
using rent.domain.Services.SendNewOrderMessage;
using rent.domain.Services.UploadImage;
using rent.infrastructure.DataAccess;
using rent.infrastructure.Repositories;
using rent.infrastructure.Security.Access.Generator;
using rent.infrastructure.Security.Access.Validator;
using rent.infrastructure.Services.LoggedUser;
using rent.infrastructure.Services.ReadNewOrderMessage;
using rent.infrastructure.Services.SendNewOrderMessage;
using rent.infrastructure.Services.UploadImage;

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
            AddAmazonSQS_S3(services, configuration);
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

            services.AddScoped<IOrderWriteOnlyRepository, OrderRepository>();
            services.AddScoped<IOrderUpdateOnlyRepository, OrderRepository>();

            services.AddScoped<INotifyDeliveryPersonWriteOnlyRepository, NotifyDeliveryPersonRepository>();
            services.AddScoped<INotifyDeliveryPersonReadOnlyRepository, NotifyDeliveryPersonRepository>();
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

        //private static void AddSendNewOrderMessage(IServiceCollection services) => services.AddScoped<ISendNewOrderMessage, SendNewOrderMessage>();

        private static void AddAmazonSQS_S3(IServiceCollection services, IConfiguration configuration)
        {
            var awsSQLConfig = new AmazonSQSConfig
            {
                ServiceURL = configuration.GetValue<string>("LocalStack:ServiceUrl"),
                AuthenticationRegion = configuration.GetValue<string>("Aws:Region")
            };

            services.AddSingleton<IAmazonSQS>(new AmazonSQSClient("test", "test", awsSQLConfig));
            services.AddScoped<ISendNewOrderMessage>(provider => new SendNewOrderMessage(
                provider.GetRequiredService<IAmazonSQS>(),
                configuration.GetValue<string>("Aws:Sqs:QueueUrl")!
            ));

            services.AddScoped<IReadNewOrderMessage>(provider => new ReadNewOrderMessage(
                provider.GetRequiredService<IAmazonSQS>(),
                configuration.GetValue<string>("Aws:Sqs:QueueUrl")!,
                provider.GetRequiredService<INotifyDeliveryPersonWriteOnlyRepository>(),
                provider.GetRequiredService<IUserReadOnlyRepository>(),
                provider.GetRequiredService<IUnitOfWork>()
            ));

            var awsS3Config = new AmazonS3Config
            {
                ServiceURL = configuration.GetValue<string>("LocalStack:ServiceUrl"),
                ForcePathStyle = true,
                AuthenticationRegion = "us-east-1"
            };
            services.AddSingleton<IAmazonS3>(new AmazonS3Client("test", "test", awsS3Config));
            services.AddScoped<IUploadImage>(provider => new UploadImage(
                provider.GetRequiredService<IAmazonS3>(),
                configuration.GetValue<string>("Aws:S3:BucketName")!
            ));

        }

    }
}
