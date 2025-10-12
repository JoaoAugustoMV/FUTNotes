using FootNotes.Core.Application;
using FootNotes.MatchManagement.Application.Commands.MatchCommands;
using FootNotes.MatchManagement.Application.Requests;
using FootNotes.MatchManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootNotes.MatchManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchManagementController(ILogger<MatchManagementController> logger, IMatchService matchService) : ControllerBase
    {       

        [HttpPost(Name = "Create Match Manually")]
        public async Task<ActionResult<Result<bool>>> CreateMatchManually(CreateMatchManuallyRequest request)
        {
            try
            {
                return Ok(await matchService.CreateMatchManually(request));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error registering user");
                return StatusCode(500, "Internal server error");
            }            
        }
    }
}
