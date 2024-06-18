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
        Task<User?> Login(LoginRequestModel loginRequest);
        
        Task<User?> FindUserByEmailAsync(string email);

        Task<JsonResult> AddUserAsync(SignupRequestModel signupRequestModel);
        Task EmailSenderWithToken(string email, string token);

        Task<JsonResult> ForgotPassWord(ForgotPasswordRequestModel forgotPasswordRequestModel);

      
        Task Save();
    }
}
