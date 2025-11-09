using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Grafana;
using Serilog.Sinks.Grafana.Loki;

namespace FootNotes.Crosscuting.Logging
{
    public static class LoggingConfiguration
    {
        public static void Configure(ConfigurationManager configuration)
        {
            string? uri = configuration["Grafana:URI"];
            string? labelKey = configuration["Grafana:LabelKey"];
            string? labelValue = configuration["Grafana:LabelValue"];
            
            if(uri is null || labelKey is null || labelValue is null)
            {                
                throw new ArgumentNullException("Grafana", "Grafana logging configuration is missing required values.");
            }

            Log.Logger = new LoggerConfiguration()                
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .Enrich.WithEnvironmentName()
                .Enrich.WithMachineName()
                .Enrich.WithThreadId()
                .Enrich.WithProcessId()
                .WriteTo.Async(wa => wa.Console())
                .WriteTo.Async(wa => wa.GrafanaLoki(
                    uri: uri,
                    labels: [
                        new LokiLabel()
                        {
                            Key = labelKey,
                            Value = labelValue
                        }],
                    batchPostingLimit: 100,
                    period: TimeSpan.FromSeconds(5)))
                .CreateLogger();
        }
    }
}
