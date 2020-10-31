using AzureFunctions.Core.Interfaces.Email;
using AzureFunctions.Infrastructure.Configuration;
using AzureFunctions.Infrastructure.Services.Email;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System.IO;

[assembly: FunctionsStartup(typeof(AzureFunctions.Starter.Startup))]

namespace AzureFunctions.Starter
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            ConfigureServices(builder.Services);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            //Configurations
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"local.settings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            // Use a singleton Configuration throughout the application
            services.AddSingleton<IConfiguration>(configuration);
            // Singleton instance. See example usage in SendGridEmailService: inject IOptions<SendGridEmailSettings> in SendGridEmailService constructor
            services.Configure<SendGridEmailSettings>(configuration.GetSection("SendGridEmailSettings"));

            // if default ILogger is desired instead of Serilog
            //services.AddLogging();

            // configure serilog
            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("C:\\Logs\\AzureFunctions.Starter\\log-StaterFunction.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
            services.AddLogging(lb => lb.AddSerilog(logger));

            //Register SendGrid Email
            services.AddScoped<IEmailService, SendGridEmailService>();

        }
    }
}
