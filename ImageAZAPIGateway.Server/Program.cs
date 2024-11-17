using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper.Contrib.Autofac.DependencyInjection;
using ImageAZAPIGateway.DependencyInjection;
using ImageAZAPIGateway.Server.Infrastructure.Filters;
using MediatR.NotificationPublishers;
using Newtonsoft.Json;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Mediatr
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblyContaining<Program>();
    cfg.NotificationPublisher = new ForeachAwaitPublisher();
});

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                builder => builder
                .SetIsOriginAllowed((host) => true)
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials()
    );
});

// Serilog
var logger = new LoggerConfiguration()
    .Enrich.WithProperty("App", "Web App Logging")
    .MinimumLevel.Debug()
    .WriteTo.File("Logs\\Log-.txt", rollingInterval: RollingInterval.Day, retainedFileCountLimit: 7)
  .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(container =>
{
    container.RegisterModule(new ApplicationModule(builder.Configuration));
    container.RegisterAutoMapper(Assembly.GetExecutingAssembly());
});

// MVC
builder.Services.AddControllers(opts =>
{
    opts.Filters.Add(typeof(GlobalExceptionFilter));
    opts.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
}).AddNewtonsoftJson(x =>
{
    x.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
    x.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
    x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    x.SerializerSettings.Formatting = Formatting.Indented;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Migrate db auto on test environments
if (builder.Configuration.GetValue<bool>("AutoDbMigration"))
{
    app.MigrateDb(builder.Configuration);
}

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
