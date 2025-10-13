using Microsoft.AspNetCore.Mvc;

namespace FootNotes.Annotations.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AnnotationSessionController : ControllerBase
    {
        
        private readonly ILogger<AnnotationSessionController> _logger;

        public AnnotationSessionController(ILogger<AnnotationSessionController> logger)
        {
            _logger = logger;
        }

    }
}
