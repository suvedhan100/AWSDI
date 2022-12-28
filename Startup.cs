using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace AWSDI
{
    public class Startup
    {
        private readonly IConfigurationRoot Configuration;

        public Startup()
        {
            Configuration = new ConfigurationBuilder() // ConfigurationBuilder() method requires Microsoft.Extensions.Configuration NuGet package
                .SetBasePath(Directory.GetCurrentDirectory())  // SetBasePath() method requires Microsoft.Extensions.Configuration.FileExtensions NuGet package
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true) // AddJsonFile() method requires Microsoft.Extensions.Configuration.Json NuGet package
                .AddEnvironmentVariables() // AddEnvironmentVariables() method requires Microsoft.Extensions.Configuration.EnvironmentVariables NuGet package
                .Build();
        }

        public IServiceProvider ConfigureServices()
        {
            var services =new ServiceCollection();

            // Add logging service
            services.AddLogging(loggingBuilder =>  // AddLogging() requires Microsoft.Extensions.Logging NuGet package
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddNLog(Path.Join(Directory.GetCurrentDirectory(), "nlog.config"));
                //loggingBuilder.AddConsole(); // AddConsole() requires Microsoft.Extensions.Logging.Console NuGet package
            });

            // Add configuration service
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IHelloService, HelloService>();



            IServiceProvider serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
