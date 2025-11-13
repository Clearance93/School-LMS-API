using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrganizationCore.Exceptions;
using OrganizationDTO;
using OrganizationDTO.Dto;
using OrganizationIInterface.IService;
using System.Text;

namespace ThutonetOrganizationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _Logger;
        private readonly IUserServiceInterface _User;

        public AuthController(IUserServiceInterface user,
                              ILogger<AuthController> logger)
        {
            _User = user;
            _Logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto dto)
        {
            try
            {
                var result = await _User.CreateUserAsync(dto);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while registering user.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("deactivate/{userId}")]
        public async Task<IActionResult> DeactivateUser([FromRoute] string userId)
        {
            try
            {
                var result = await _User.DeactivateUserAsync(userId);

                if (!result)
                {
                    return NotFound("User not found or already deactivated.");
                }

                return Ok("User deactivated successfully.");
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while deactivating user.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPut("update/{userId}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string userId, [FromBody] UpdateUserDto dto)
        {
            try
            {
                var result = await _User.UpdateUserAsync(userId, dto);

                if (result == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while updating user.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("active-users")]
        public async Task<IActionResult> GetAllActiveUsers()
        {
            try
            {
                var result = await _User.GetAllActiveUsersAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while retrieving active users.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpGet("user-by-email/{email}")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            try
            {
                var result = await _User.GetUserByEmailAsync(email);

                if (result == null)
                {
                    return NotFound("User not found.");
                }

                return Ok(result);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while retrieving user by email.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> AuthenticateUser([FromBody] UserLoginDto dto)
        {
            try
            {
                var result = await _User.AuthenticateUserAsync(dto);

                if (result == null)
                {
                    return Unauthorized("Invalid email or password.");
                }
                return Ok(result);
            }
            catch (AuthenticationException ex)
            {
                _Logger.LogWarning(ex, "Authentication failed for user: {Email}", dto.Email);

                return Unauthorized(new { message = ex.Message });
            }
            catch (EmailNotConfirmedException ex)
            {
                _Logger.LogWarning(ex, "Email not Confirmed for user: {email}:", dto.Email);

                return StatusCode(StatusCodes.Status423Locked, new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error occurred while authenticating user.");

                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                await _User.GeneratePasswordResetTOkenRepositoryAsnc(dto.Email);

                return Ok("Password reset token was aend to your email");
            }
            catch (Exception exception)
            {
                _Logger.LogError($"Failed to send the password token: {exception.Message}");

                throw;
            }
        }

        [HttpPut("confirm-email/{userId}-token/{token}")]
        public async Task<IActionResult> EmailConfirmation(string userId, string token)
        {
            try
            {
                //var originalToken = FromBase64UrlSafe(token);

                var emailConfirmation = await _User.EmailConfirmationAsync(userId, token);

                return Ok(emailConfirmation);
            }
            catch (Exception exception)
            {
                throw new OrganizationCore.Exceptions.InvalidOperationException($"Failed to confrim the email. {exception.Message}");
            }
        }

        //private string FromBase64UrlSafe(string token)
        //{
        //    var base64 = token
        //        .Replace('-', '+')
        //        .Replace('_', '/');

        //    switch (base64.Length % 4)
        //    {
        //        case 2: base64 += "==";
        //            break;
        //        case 3: base64 += "=";
        //            break;
        //    }

        //    var bytes = Convert.FromBase64String(base64);

        //    return Encoding.UTF8.GetString(bytes);
        //}
    }
}
