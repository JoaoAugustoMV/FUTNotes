using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootNotes.MatchManagement.Adapters.MatchProviders.Dojo
{
    public record DojoConfigs        
    {
        public string URI_Base {get;init;}
        public string Path_NextMatches {get;init;}
        public string API_Key { get; init; }
        public string Header_API_Key { get; init; }
        public DojoConfigCompetition[] AvailableCompetition { get; init; }
        public DojoConfigs() { }

        public DojoConfigs(string URI_Base, string Path_NextMatches, DojoConfigCompetition[] AvailableCompetition)
        {            
            this.URI_Base = URI_Base;
            this.Path_NextMatches = Path_NextMatches;
            this.AvailableCompetition = AvailableCompetition;
        }

    }

    public record DojoConfigCompetition(
        string Name,
        Guid InternalId,
        int ExternalId,
        int ExternalCurrentSessionId
        );
    
}
