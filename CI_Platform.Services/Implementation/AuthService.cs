using AutoMapper;
using CI_Platform.Models;
using CI_Platform.Models.DBContext;
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using CI_Platform.Repository.Implementation;
using CI_Platform.Repository.Interface;
using CI_Platform.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _context;
        private readonly IAuthRepo _repo;
        private readonly IJWTService _jwtService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        public AuthService(AppDbContext context, IMapper mapper, IAuthRepo authRepo, IConfiguration configuration, IJWTService _jwtService)
        {
            _context = context;
            _repo = authRepo;
            this._jwtService = _jwtService;
            this.mapper = mapper;
            this.configuration = configuration;
        }


        /// <summary>
        /// Login Service
        /// </summary>
        /// <param name="loginRequestModel"></param>
        /// <returns> JWT Token</returns>
        #region Login
        public async Task<JsonResult> Login(LoginRequestModel loginRequestModel)
        {
            var user = await _repo.Login(loginRequestModel);
           
           if(user==null)
            {
                return new JsonResult(new Response<LoginResponseModel>()
                {
                    Data = null,
                    Message = "Login Failed",
                    IsSuccess = false,
                    StatusCode = StatusCodes.Status400BadRequest,
                });
           }

            var jwtToken = await _jwtService.GenerateJWTToken(user);

            return new JsonResult(new Response<string>()
            {
                Data = jwtToken,
                Message = "Login Successfully",
                IsSuccess = true,
                StatusCode = 200,
            });

        }
        #endregion

        /// <summary>
        /// Register Service
        /// </summary>
        /// <param name="signupRequestModel"></param>
        /// <returns></returns>
        #region Signup

        public async Task<JsonResult> RegisterUserAsync(SignupRequestModel signupRequestModel)
        {
            return await _repo.AddUserAsync(signupRequestModel);
        }

        #endregion

        /// <summary>
        /// Forgot Password Service
        /// </summary>
        /// <param name="forgotPasswordRequestModel"></param>
        /// <returns> Token </returns>
        #region ForgotPassword
        public async Task<JsonResult> ForgotPassword(ForgotPasswordRequestModel forgotPasswordRequestModel)
        {
            
            var user = await _repo.ForgotPassWord(forgotPasswordRequestModel);


            var token = await _jwtService.GenerateJWTToken(user.Value as User);
            if(token == null)
            {
                return new JsonResult(new Response<string>()
                {
                    Data = null,
                    Message = "Toekn not Created",
                    IsSuccess = false,
                    StatusCode = 400,
                });

            }
            await _repo.EmailSenderWithToken(forgotPasswordRequestModel.Email!, token);
            return new JsonResult(new Response<string>()
            {
                Data = token,
                Message = "Email Send Successfully",
                IsSuccess = true,
                StatusCode = 200,
            });

        }

        #endregion


        /// <summary>
        /// Reset Password Service
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <param name="resetPasswordRequestModel"></param>
        /// <returns></returns>
        #region Reset Password
        public async Task<JsonResult> ResetPassword(string token, string email, ResetPasswordRequestModel resetPasswordRequestModel)
        {
            
            try
            {
                var user = await _repo.FindUserByEmailAsync(email);
                if (user == null)
                {
                    return new JsonResult(new Response<string>
                    {
                        IsSuccess = false,
                        Message = "Invalid credential or user doesn't exist",
                        StatusCode =404,
                    });
                }
                if (!IsTokenValid(email, token))
                {
                    return new JsonResult(new Response<string>
                    {
                        IsSuccess = false,
                        Message = "Invalid token or token has expired",
                        StatusCode = 404,
                    });
                }
                user.Password = resetPasswordRequestModel.Password;
                await _repo.Save();
                return new JsonResult(new Response<string>
                {
                    IsSuccess = true,
                    Message = "Password changed succussfully!",
                    StatusCode =200,
                });
            }
            catch
            {
                return new JsonResult(new Response<string>
                {
                    IsSuccess = false,
                    Message = "Internal server error",
                    StatusCode = 400,
                });
            }
        }
        #endregion
        /// <summary>
        /// Valid Token Like Exp Time and valid user for token
        /// </summary>
        /// <param name="email"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        #region Check Token Valid 
        public bool IsTokenValid(string email, string token) 
        {
            try
            {
                ValidateToken(token, out JwtSecurityToken jwtToken);
                if (jwtToken == null)
                {
                    return false;
                }
                var tokenEmail = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "Email")!.Value;
                var expTime = jwtToken.Claims.FirstOrDefault(claim => claim.Type.Equals("exp"))!.Value;
                if (tokenEmail != null && tokenEmail == email)
                {
                    return true;
                }
                return false;
            }
            catch { throw new Exception(); }
        }

        #endregion


        public bool ValidateToken(string token, out JwtSecurityToken jwtSecurityToken)
        {
            try
            {
                jwtSecurityToken = null!;
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(configuration["ApplicationSettings:JWT_Secret"]!);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                jwtSecurityToken = (JwtSecurityToken)validatedToken;

                if (jwtSecurityToken == null)
                {
                    return false;
                }
                return true;
            }
            catch (SecurityTokenExpiredException)
            {
                jwtSecurityToken = null!;
                // Token has expired
                return false;
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}
