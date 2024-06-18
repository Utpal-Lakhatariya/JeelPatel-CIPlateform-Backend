using CI_Platform.Models;
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Interface
{
    public interface IAuthService
    {
         Task<JsonResult> Login(LoginRequestModel loginRequestModel);

       // Task<bool> IsUserExist(string email);
        Task <JsonResult> RegisterUserAsync(SignupRequestModel signupRequestModel);

        Task<JsonResult> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel);

        Task<JsonResult> ResetPassword(string token,string email, ResetPasswordRequestModel resetPasswordRequestModel);
        bool IsTokenValid(string email, string token);
        bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken);
    }
}
