using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.IO;

namespace Circle.Library.Api
{
    /// <summary>
    ///
    /// </summary>
    public static class Program
    {
        static IConfiguration Configuration;

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        /// 
        [Obsolete]
        public static void Main(string[] args)
        {
            Configuration = GetConfiguration();

            CreateMSSqlLogger();

            CreateHostBuilder(args).Build().Run();
        }

        private static IConfiguration GetConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
            var built = builder.Build();

            return built;
        }

        [Obsolete]
        public static void CreateMSSqlLogger()
        {

            var connectionString = Configuration.GetSection("SeriLogConfigurations:MsSqlConfiguration:ConnectionString").Value;
            var tableName = "Logs";

            var columnOption = new ColumnOptions();
            columnOption.Store.Remove(StandardColumn.MessageTemplate);

            columnOption.AdditionalDataColumns = new Collection<DataColumn>
                            {
                                new DataColumn {DataType = typeof (string), ColumnName = "OtherData"},
                            };

            Log.Logger = new LoggerConfiguration()
                            .MinimumLevel.Warning()
                            .MinimumLevel.Override("SerilogDemo", LogEventLevel.Information)
                            .WriteTo.MSSqlServer(connectionString, tableName,
                                    columnOptions: columnOption

                                    )
                            .CreateLogger();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                });
    }
}
