using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TicketBookingProject.Data.Dto;
using TicketBookingProject.IRepositry;
using TicketBookingProject.Models;

namespace TicketBookingProject.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserCredentialRepository _userCredentialRepositry;

        public AuthController(IUserCredentialRepository userCredentialRepositry)
        {
            _userCredentialRepositry = userCredentialRepositry;
        }

        #region AuthorizationRegister
        [HttpPost("Register")]
        public IActionResult RegisterUser([FromBody] UsersCredential users)
        {
            _userCredentialRepositry.AddUser(users);
            return Ok(new { Message = "User Created Success" });
        }
        #endregion

        #region Authorization
        [HttpPost("authenticate")]
        public IActionResult Auth([FromBody] UserCredentialsDto users)
        {
            var jwtToken = _userCredentialRepositry.AuthenticateUser(users);
            if (jwtToken == null)
            {
                return Unauthorized();
            }
            return Ok(new { Token = jwtToken });
        }
        #endregion
        [HttpPut("UpdatePassword")]
        public IActionResult UpdatePassword([FromBody] UpdatePasswordDto updatePasswordDto) {
            _userCredentialRepositry.UpdatePassword(updatePasswordDto.UserNameOrEmail, updatePasswordDto.NewPassword);

            {
                return Ok(new { Message = "Password Updated Successfully" });
            }
        }
    }
}
