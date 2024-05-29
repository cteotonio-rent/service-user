using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using rent.application.Services.AutoMapper;
using rent.application.Services.Cryptography;
using rent.application.UseCases.Login.DoLogin;
using rent.application.UseCases.Motorcycle.Get;
using rent.application.UseCases.Motorcycle.Register;
using rent.application.UseCases.Motorcycle.Update;
using rent.application.UseCases.Order.AcceptOrder;
using rent.application.UseCases.Order.DeliverOrder;
using rent.application.UseCases.Order.GetOrderDeliverMan;
using rent.application.UseCases.Order.Register;
using rent.application.UseCases.Rental.Register;
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
            #region Login
            services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
            #endregion Login

            #region User
            services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
            services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
            services.AddScoped<IUpdateUserUseCase, UpdateUserUseCase>();
            services.AddScoped<IUpdateUserImageUseCase, UpdateUserImageUseCase>();
            #endregion User

            #region Motorcycle
            services.AddScoped<IRegisterMotorcycleUseCase, RegisterMotorcycleUseCase>();
            services.AddScoped<IGetMotorcycleUseCase, GetMotorcycleUseCase>();
            services.AddScoped<IUpdateMotorcycleLicensePlateUseCase, UpdateMotorcycleLicensePlateUseCase>();
            #endregion Motorcycle

            #region Rental
            services.AddScoped<IRegisterRentalUseCase, RegisterRentalUseCase>();
            #endregion Rental

            #region Order
            services.AddScoped<IGetOrderDeliveryPersonUseCase, GetOrderDeliveryPersonUseCase>();
            services.AddScoped<IAcceptOrderUseCase, AcceptOrderUseCase>();
            services.AddScoped<IRegisterOrderUseCase, RegisterOrderUseCase>();
            services.AddScoped<IDeliverOrderUseCase, DeliverOrderUseCase>();
            #endregion Order


        }

        private static void AddPasswordEcripter(IServiceCollection services, IConfiguration configuration)
        {
            var additionallKey = configuration.GetValue<string>("Settings:Passsword:AdditionalKey");
            services.AddScoped(options => new PasswordEncripter(additionallKey!));
        }
    }
}
