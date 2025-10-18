using FootNotes.Annotations.Application.Requests;
using FootNotes.Annotations.Application.Services;
using FootNotes.Core.Application;
using Microsoft.AspNetCore.Mvc;

namespace FootNotes.Annotations.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnotationSessionController(ILogger<AnnotationSessionController> logger, IAnnotationSessionService annotationSessionService) : ControllerBase
    {
        [HttpPost("New")]
        public async Task<IActionResult> CreateNewSession(CreateNewAnnotationSessionRequest request)
        {
            try
            {
                Result<Guid> response = await annotationSessionService.CreateNewAnnotationSessionAsync(request);

                if (response.Successed)
                {
                    return Ok(response);
                }

                return BadRequest(response);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on Create new session");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
