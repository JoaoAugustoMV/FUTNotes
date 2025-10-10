using FootNotes.Core.Application;
using FootNotes.Crooscutting.Utils;
using FootNotes.IAM.Application.Queries.Interfaces;
using FootNotes.IAM.Application.Queries.ViewModels;
using FootNotes.IAM.Application.Request;
using FootNotes.IAM.Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace FootNotes.IAM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IUserQueries userQueries, JwtService jwtService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<Result<string>>> Login(UserLoginRequest request)
        {
            UserViewModel? user = await userQueries.GetUserViewModelByEmail(request.Email);

            if (user != null)
            {
                if (PasswordHelper.VerifyPassword(request.Password, user.Password))
                {
                    string token = jwtService.GenerateToken(user.Username);

                    return Ok(Result<string>.Success(token));
                }

            }

            return Unauthorized(Result<string>.Failure("Login Failed"));
        }
    }
}
