using CI_Platform.Models;
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface IAuthRepo
    {
        // login
        Task<User?> Login(LoginRequestModel loginRequest);
        
        Task<User?> FindUserByEmailAsync(string email);

        //Register
        Task<JsonResult> AddUserAsync(SignupRequestModel signupRequestModel);
        
        //Email Send With Token
        Task EmailSenderWithToken(string email, string token);

        // Forgot Password
        Task<JsonResult> ForgotPassWord(ForgotPasswordRequestModel forgotPasswordRequestModel);

        Task Save();
    }
}
