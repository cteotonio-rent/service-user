using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rent.application.Services.AutoMapper;
using rent.application.Services.Cryptography;
using rent.application.UseCases.Login.DoLogin;
using rent.application.UseCases.Motorcycle.Get;
using rent.application.UseCases.Motorcycle.Register;
using rent.application.UseCases.User.Profile;
using rent.application.UseCases.User.Register;
using rent.application.UseCases.User.Update;

namespace rent.application
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
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();

            services.AddScoped<IRegisterMotorcycleUseCase, RegisterMotorcycleUseCase>();
            services.AddScoped<IGetMotorcycleUseCase, GetMotorcycleUseCase>();
        }

        private static void AddPasswordEcripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionallKey = configuration.GetValue<string>("Settings:Passsword:AdditionalKey");
            services.AddScoped(options => new PasswordEncripter(additionallKey!));
        }
    }
}
