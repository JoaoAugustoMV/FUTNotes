using FootNotes.MatchManagement.Integration;
using Grpc.Core;
using FootNotes.Integration;
using FootNotes.MatchManagement.Domain.Repository;


namespace FootNotes.MatchManagement.Integration.Services
{
    public class MatchIntegrationGrpcService(IMatchRepository matchRepository) : MatchIntegrationService.MatchIntegrationServiceBase
    {
        public override async Task<MatchExistsResponse> MatchExists(MatchExistsRequest request, ServerCallContext context)
        {
            bool validGuid = Guid.TryParse(request.ExternalMatchId, out Guid matchId);
            
            if (!validGuid)
            {
                return new MatchExistsResponse
                {
                    Exists = false
                };
            }
            
            return new MatchExistsResponse
            {
                Exists = await matchRepository.ExistsId(matchId)
            };
        }
    }
}
