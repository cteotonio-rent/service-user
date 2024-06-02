using Amazon.SQS;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using rent.application;
using rent.application.UseCases.NotifyDeliveryPerson.Register;
using rent.infrastructure;

var builder = Host.CreateDefaultBuilder(args)
     .ConfigureAppConfiguration((hostingContext, config) =>
     {
         var env = hostingContext.HostingEnvironment;
         //env.EnvironmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production";

         // Carrega o arquivo 'appsettings.json' e, em seguida, o arquivo 'appsettings.{env.EnvironmentName}.json'
         config.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true);
     })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddApplication(hostContext.Configuration);
        services.AddInfrastructure(hostContext.Configuration);
        services.AddTransient<NotifyDeliveryPersonUseCase>();
        //services.AddHostedService<Worker>();
    });


var host = builder.Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    var processMessagesService = services.GetRequiredService<NotifyDeliveryPersonUseCase>();
    await processMessagesService.Execute();
}
catch (AmazonSQSException ex)
{
    Console.WriteLine($"Error sending message to SQS: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error sending message to SQS: {ex.Message}");
}

