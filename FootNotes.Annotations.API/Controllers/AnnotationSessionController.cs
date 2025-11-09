using FootNotes.Annotations.Application.QueryStack.Queries;
using FootNotes.Annotations.Application.QueryStack.ViewModels;
using FootNotes.Annotations.Application.Requests;
using FootNotes.Annotations.Application.Services;
using FootNotes.Core.Application;
using FootNotes.Core.Data.Communication;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FootNotes.Annotations.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnotationSessionController(
        ILogger<AnnotationSessionController> logger,
        IAnnotationSessionService annotationSessionService,
        IMediatorHandler mediatorHandler
        ) : ControllerBase
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

        [HttpPost("AddAnnotation")]
        public async Task<IActionResult> AddAnnotation(AddAnnotationRequest request)
        {
            try
            {
                Result<Guid> result = await annotationSessionService.AddAnnotationAsync(request);

                if (result.Successed)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on Add annotation");
                return StatusCode(500, "Internal server error");                
            }
            
        }

        [HttpGet("{sessionId}")]
        public async Task<IActionResult> GetAnnotationSessionById(Guid sessionId)
        {
            try
            {
                AnnotationSessionViewModel? result = await mediatorHandler.Query(new GetAnnotationSessionByIdQuery(sessionId));
                
                if (result != null)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error on Add annotation");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
