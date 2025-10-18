using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FootNotes.Integration;
using Grpc.Net.Client;
using static FootNotes.Integration.MatchIntegrationService;



namespace FootNotes.Annotations.Application.Services.Impls
{
    public class AnnotationMatchService(MatchIntegrationServiceClient client) : IAnnotationMatchService
    {
        public async Task<bool> ExistsMatchId(Guid matchId)
        {
            MatchExistsResponse response = await client.MatchExistsAsync(new MatchExistsRequest { ExternalMatchId = matchId.ToString() });
            return response.Exists;
        }
        
    }
}
