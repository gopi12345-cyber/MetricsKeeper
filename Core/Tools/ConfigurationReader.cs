using System;
using Microsoft.Extensions.Configuration;

namespace Core.Tools
{
    public class ConfigurationReader
    {
        public IConfiguration Configuration { get; }
        public ConfigurationReader()
        {
			var builder = new ConfigurationBuilder()
			   .SetBasePath(AppContext.BaseDirectory)
			   .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
			   .AddEnvironmentVariables();
			Configuration = builder.Build();
        }

    }
}
