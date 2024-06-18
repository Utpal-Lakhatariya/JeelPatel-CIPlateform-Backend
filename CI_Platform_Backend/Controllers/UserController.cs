
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using CI_Platform.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CI_Platform_Backend.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;
        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Login Controller to handle login service
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns></returns>
        #region Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequestModel)
        {
            return await _authService.Login(loginRequestModel);

        }
        #endregion


        /// <summary>
        /// Register Controller to handle Register service
        /// </summary>
        /// <param name="signupRequestModel"></param>
        /// <returns></returns>

        #region Register
        [HttpPost("register")]

        public async Task<IActionResult> Register(SignupRequestModel signupRequestModel)
        {
            return await _authService.RegisterUserAsync(signupRequestModel);
        }

        #endregion


        /// <summary>
        /// Forgot Passwprd Controller to handle Forgot password Service
        /// </summary>
        /// <param name="forgotPasswordRequestModel"></param>
        /// <returns></returns>
        #region Forgot Password

        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
           return await _authService.ForgotPassword(forgotPasswordRequestModel);
           
        }

        #endregion
        

        /// <summary>
        /// Reset Passwprd Controller to handle Reset password Service
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <param name="resetPasswordRequestModel"></param>
        /// <returns></returns>
        #region Reset Password

        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromQuery] string token, [FromQuery] string email,ResetPasswordRequestModel resetPasswordRequestModel)
        {
                
            return await _authService.ResetPassword(token, email, resetPasswordRequestModel);
            
        }

        #endregion

    }
}
