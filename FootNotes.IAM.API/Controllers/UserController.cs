using FootNotes.Core.Application;
using FootNotes.IAM.Application.Commands;
using FootNotes.IAM.Application.Interfaces;
using FootNotes.IAM.Domain;
using Microsoft.AspNetCore.Mvc;

namespace FootNotes.IAM.API.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class UserController(ILogger<UserController> logger, IUserService userService) : ControllerBase
    {
        [HttpPost(Name = "Register")]
        public async Task<ActionResult<UserStatusResponse>> Register(UserRegisterCommand userRegisterCommand)
        {
            try
            {
                Result<UserStatusResponse> result = await userService.RegisterUserAsync(userRegisterCommand);

                if(result.Successed)
                {
                    logger.LogInformation("User {userRegisterCommand.Username} registered successfully.", userRegisterCommand.Username);
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error registering user");
                return StatusCode(500, "Internal server error");
            }
                        
        }

        [HttpPut(Name = "Update")]
        public async Task<ActionResult<UserStatusResponse>> Update(UserUpdateCommand userUpdateCommand)
        {
            try
            {
                Result<UserStatusResponse> result = await userService.UpdateUserAsync(userUpdateCommand);

                if (result.Successed)
                {
                    logger.LogInformation("User {userUpdateCommand.Username} updated successfully.", userUpdateCommand.Username);
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error registering user");
                return StatusCode(500, "Internal server error");
            }

        }

    }
}
