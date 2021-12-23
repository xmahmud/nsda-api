//using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Utility.Environment
{
    public class EnvironmentHelper
    {
        public static IConfigurationBuilder GetAppSettings(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            return builder;
        }

        public static string GetConfigurationPath(string environment)
        {            
            switch (environment)
            {
                case "Production":
                    return Path.Combine("configuration", "configuration.Production.json");
                case "Staging":
                    return Path.Combine("configuration", "configuration.Staging.json");
                default:
                    return Path.Combine("configuration", "configuration.json");
            }
        }
    }
}
