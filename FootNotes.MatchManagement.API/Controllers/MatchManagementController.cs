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

        [HttpPost("Manual", Name = "Create Match Manually")]
        public async Task<ActionResult<Result<Guid>>> CreateMatchManually(CreateMatchManuallyRequest request)
        {
            try
            {
                Result<Guid> result = await matchService.CreateMatchManually(request);
                logger.LogInformation($"Result: {result}");
                if (result.Successed)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on create match manualy");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
