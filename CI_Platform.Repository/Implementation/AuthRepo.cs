using CI_Platform.Models.RequestModel;
using CI_Platform.Models;
using CI_Platform.Repository.Interface;
using CI_Platform.Models.DBContext;
using Microsoft.EntityFrameworkCore;
using CI_Platform.Models.ResponseModel;
using AutoMapper;
using System.Net.Mail;
using System.Net;
using System.Web;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform.Repository.Implementation
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        public AuthRepo(AppDbContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

       
        #region Login
        public async Task<User?> Login(LoginRequestModel loginRequest)
        {
            //  retrieve the user from the database 
            User? user = await _context.Users.FirstOrDefaultAsync(i => i.Email == loginRequest.Email);
            if (user == null)
            {
                return null;
            }
            // If a user is found, check if the provided password matches the user's password.
            if (user != null && user.Password == loginRequest.Password)
            {
                return user;
            }
            return null;
        }
        #endregion

        #region FindEmail
        public async Task<User?> FindUserByEmailAsync(string email)
        {
            try
            {
                User? user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
                return user;
            }
            catch
            {
                return null;

            }
        }
        #endregion

        /// <summary>
        /// Register Repo
        /// </summary>
        /// <param name="signupRequestModel"></param>
        /// <returns>User table</returns>
        #region Signup
        public async Task<JsonResult> AddUserAsync(SignupRequestModel signupRequestModel)
        
        {
            try
            {
                var userCheck = await FindUserByEmailAsync(signupRequestModel.Email!);
                if (userCheck != null)
                {
                    return new JsonResult(new Response<string>()
                    {
                        Data = null,
                        Message = "User Have alredy Account",
                        IsSuccess = false,
                        StatusCode = 404,
                    });
                }

                var data = mapper.Map<User>(signupRequestModel);
                data.Status = 0;
                data.CityId = 1;
                data.CountryId = 1;
                await _context.AddAsync(data);
                await _context.SaveChangesAsync();
                return new JsonResult(new Response<User>()
                {
                    Data = data,
                    Message = "Register Successfully ",
                    IsSuccess = true,
                    StatusCode = 200,
                });
            }
            catch
            {
                return new JsonResult(new Response<string>()
                {
                    Data = null,
                    Message = "Something Went Wrong",
                    IsSuccess = false,
                    StatusCode = 404,
                });
            }
        }

        #endregion

        /// <summary>
        /// Send Email with Token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        #region EmailSend
        public Task EmailSenderWithToken(string email, string token)
        {
            var subject = "Password Reset";
            var resetLink = $"http://localhost:4200/resetPassword?email={email}&token={HttpUtility.UrlEncode(token)}";
            var message = $"Click the following link to reset your password: {resetLink}";

            var mail = "tatva.dotnet.jeelpatel@outlook.com";
            var password = "hfahrvfzkgvxonmy";

            
            SmtpClient smtpClient = new SmtpClient("smtp.office365.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(mail, password)
            };

            MailMessage mailMessage = new MailMessage(from: mail, to: email, subject, message);

            smtpClient.SendMailAsync(mailMessage);
            return Task.CompletedTask;
        }

        #endregion

        /// <summary>
        /// Forgot Password Repo
        /// </summary>
        /// <param name="forgotPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<JsonResult> ForgotPassWord(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            var validUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == forgotPasswordRequestModel.Email);

            if (validUser!=null)
            {
                return new JsonResult(validUser);
            }
            return new JsonResult(new Response<string>()
            {
                Data = null,
                Message = "User Not Exist",
                IsSuccess = false,
                StatusCode = 400,
            });
        }

        /// <summary>
        /// To Save Data into Database from service
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task Save()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

    }
}
