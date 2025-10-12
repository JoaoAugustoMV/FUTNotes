using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Json;

namespace FootNotes.Crosscuting.Logging
{
    public static class LoggingConfiguration
    {
        public static void Configure(ConfigurationManager configuration)
        {
            Log.Logger = new LoggerConfiguration()                
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .WriteTo.Async(wa => wa.Console())
                .WriteTo.Async(wa => wa.File(new JsonFormatter(), "logs/log-.txt", rollingInterval: RollingInterval.Day))                
                .CreateLogger();
        }
    }
}
