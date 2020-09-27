using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CLINT
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Application Starting");

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IPrintService, PrintService>();
                    services.AddTransient<IGreetingService, GreetingService>();

                })
                .UseSerilog()
                .Build();

            //Understand input arguments string so that args[0] is name of Service and args[1] is input to the Service
            switch (args[0])
            {
                case "Greet":
                    var svcg = ActivatorUtilities.CreateInstance<GreetingService>(host.Services, args[1], args[2]);
                    svcg.Run();
                    break;

                case "Print":
                    var svcp = ActivatorUtilities.CreateInstance<PrintService>(host.Services);
                    svcp.Run(args[1]);
                    break;


            }

            //var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services, args[0], args[1]);

            //svc.Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }

    }
}
