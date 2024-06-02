using rent.api.Converters;
using rent.api.Filters;
using rent.api.Middleware;
using rent.api.Token;
using rent.application;
using rent.domain.Security.Tokens;
using rent.infrastructure;
using Serilog.Formatting.Compact;
using Serilog.Sinks.GrafanaLoki;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

ConfigureLog(builder.Configuration.GetSection($"Grafana:UrlLoki").Value!);

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new StringConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt => {
    opt.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { 
        Title = "Rent API", 
        Version = "v1",
        Description = "API for Rent Service -> " + builder.Configuration.GetSection($"ConnectionStrings:Connection").Value,
    });

    

    opt.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme. " +
        " Enter 'Bearer' [space] and then your token in the text input below. " + 
        " Example: 'Bearer xxxxx'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    opt.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = Microsoft.OpenApi.Models.ParameterLocation.Header
            },
            new List<string> { }
        }
    });
});
builder.Services.AddMvc(options => options.Filters.Add(new ExceptionFilter()));

builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddScoped<ITokenProvider, HttpContexTokenValue>();
builder.Services.AddRouting(options => options.LowercaseUrls = true);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSerilog(Log.Logger);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseMiddleware<CultureMiddleware>();

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void ConfigureLog(string urlLoki)
{
    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "Rent-Service")
        .Enrich.WithProperty("Environment", "Development")
        .WriteTo.Console(new RenderedCompactJsonFormatter())
        .WriteTo.GrafanaLoki(
            urlLoki,
            null,
            new Dictionary<string, string>() { { "app", "Rent-Service" } }, // Global labels
            Serilog.Events.LogEventLevel.Information,
            GrafanaLokiHelpers.DefaultOutputTemplate,
            null,
                    null,
                    null,
                    null,
                    1000,
                    null,
                    null,
                    TimeSpan.FromSeconds(2),
                    null,
                    null,
                    null,
                    true)
        .CreateLogger();
}

public partial class Program()
{

}
