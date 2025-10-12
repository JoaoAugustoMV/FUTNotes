using FootNotes.Core.Application;
using FootNotes.MatchManagement.Application.Requests;
using FootNotes.MatchManagement.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootNotes.MatchManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MatchEventsController(ILogger<MatchEventsController> logger, IMatchService matchService): ControllerBase
    {
        [HttpPut("UpdateStatus")]
        public async Task<ActionResult<string>> UpdateStatus(UpdateStatusMatchRequest request)
        {
            try
            {                
                Result<bool> resp = await matchService.ChangeMatchStatus(request);
                if (resp.Successed)
                {
                    return Ok(resp);
                }

                return BadRequest(resp);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on UpdateStatus");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("UpdateScore")]
        public async Task<ActionResult<Result<bool>>> UpdateScore(UpdateScoreMatchRequest request)
        {
            try
            {
                Result<bool> resp = await matchService.UpdateMatchScore(request);

                if (resp.Successed)
                {
                    return Ok(resp);
                }

                return BadRequest(resp);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on UpdateScore");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
