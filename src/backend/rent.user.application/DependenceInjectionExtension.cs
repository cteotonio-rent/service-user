using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rent.user.application.Services.AutoMapper;
using rent.user.application.Services.Cryptography;
using rent.user.application.UseCases.User.Register;

namespace rent.user.application
{
    public static class DependenceInjectionExtension
    {
        public static void AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AddAutoMapper(services);
            AddUseCases(services);
            AddPasswordEcripter(services, configuration);
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            services.AddScoped(option => new AutoMapper.MapperConfiguration(option =>
            {
                option.AddProfile(new AutoMapping());
            }).CreateMapper());
        }

        private static void AddUseCases(IServiceCollection services)
        {
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        }

        private static void AddPasswordEcripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionallKey = configuration.GetValue<string>("Settings:Passsword:AdditionalKey");
            services.AddScoped(options => new PasswordEncripter(additionallKey!));
        }
    }
}
