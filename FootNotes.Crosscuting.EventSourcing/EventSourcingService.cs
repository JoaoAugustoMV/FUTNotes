using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KurrentDB.Client;
using KurrentDB;
using Microsoft.Extensions.Configuration;

namespace FootNotes.Crosscuting.EventSourcing
{
    public class EventSourcingService : IEventSourcingService
    {
        private readonly KurrentDBClient _client;

        public EventSourcingService(IConfiguration configuration)
        {            
            string? connectionString = configuration["KurrentConnString"];
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException("KurrentDB connection string is not configured.");
            }

            _client = new KurrentDBClient(KurrentDBClientSettings.Create(connectionString));            
        }

        public KurrentDBClient GetClient() => _client;        
    }
}
