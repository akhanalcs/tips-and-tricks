using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            //var pathToContentRoot = Path.GetDirectoryName(pathToExe);
            var environmentName = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "EMPTYENVIRONMENTNAME";

            var config = new ConfigurationBuilder()
                //.SetBasePath(pathToContentRoot)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            ConfigureLogger(config);

            try
            {
                Serilog.Log.Information("Starting web host");
                Serilog.Log.Information($"Application Starting: Version: {System.Reflection.Assembly.GetEntryAssembly()?.GetName().Version}");
                Serilog.Log.Information($"Application Running from: {AppDomain.CurrentDomain.BaseDirectory}");
                Serilog.Log.Information($"GetEnvironmentVariable(\"DOTNET_ENVIRONMENT\") returned: {environmentName}");
                Serilog.Log.Information($"Application will be using: 'appsettings.{environmentName}.json' file (if it is available). If not, it'll be using the default 'appsettings.json' file.");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Serilog.Log.Information("Web host stopped.");
                Serilog.Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        //This method is added to override the config of default logger
        public static void ConfigureLogger(IConfiguration configuration)
        {
            // Do not remove these commented code. They're useful for reference.
            //Level:u4 means only show 4 characters. If it's Information, it'll only show INFO.
            //const string loggerTemplate = @"{Timestamp:yyyy-MM-dd HH:mm:ss} {MachineName} {EnvironmentUserName} [{Level:u4}] <{ThreadId}> [{SourceContext:l}] {Message:lj}{NewLine}{Exception}";
            //var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //var logFile = Path.Combine(baseDirectory, "App_Data", "logs", "log.txt");
            //Log.Logger = new LoggerConfiguration()
            //                 .MinimumLevel.Override("Microsoft", LogEventLevel.Warning) //Only log events from Microsoft that are Warning.
            //                 .Enrich.WithThreadId()
            //                 .Enrich.WithMachineName()
            //                 .Enrich.WithEnvironmentUserName()
            //                 .Enrich.FromLogContext()
            //                 .WriteTo.Console(LogEventLevel.Information, loggerTemplate, theme: AnsiConsoleTheme.Literate)
            //                 .WriteTo.File(logFile, LogEventLevel.Information, loggerTemplate, rollingInterval: RollingInterval.Day, retainedFileCountLimit: 90) //Keep files for 90 days.
            //                 .CreateLogger();

            //Now all the configuration is being read from appsettings.json file! How cool is that:D - AshishK
            Serilog.Log.Logger = new LoggerConfiguration()
                                    .ReadFrom.Configuration(configuration)
                                    .CreateLogger();

            Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
        }
    }
}
