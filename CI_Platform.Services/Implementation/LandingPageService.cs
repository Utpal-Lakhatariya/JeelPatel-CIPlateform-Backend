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
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Implementation
{
    public class LandingPageService : ILandingPageService
    {


        private readonly AppDbContext _context;
        private readonly ILandingPageRepo _repo;
        private readonly IJWTService _jwtService;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private static readonly HashSet<string> ImageExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tiff", ".ico", ".svg", ".webp"
};

        private static readonly HashSet<string> VideoExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
{
    ".mp4", ".mov", ".wmv", ".flv", ".avi", ".mkv", ".webm", ".mpeg", ".mpg", ".m4v"
};
        public LandingPageService(AppDbContext context, IMapper mapper, ILandingPageRepo landingPageRepo, IConfiguration configuration, IJWTService _jwtService)
        {
            _context = context;
            _repo = landingPageRepo;
            this._jwtService = _jwtService;
            this.mapper = mapper;
            this.configuration = configuration;
        }


        #region City By Country Id

        /// <summary>
        /// Get Cities from country
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public Task<JsonResult> GetCityById(Int64 id)
        {
            var data = _repo.GetCityById(id);
            if (data != null)
            {
                return Task.FromResult(new JsonResult(new Response<ICollection<DropdownResponseModel>>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = "Get Cities",
                    StatusCode = StatusCodes.Status200OK,
                }));
            }
            return Task.FromResult(new JsonResult(new Response<string>
            {
                Data = null,
                IsSuccess = false,
                Message = "something went wrong",
                StatusCode = StatusCodes.Status400BadRequest,
            }));
        }

        #endregion


        //-------------------------------------------------------------------------Get Mission Data--------------------------------------------------------------------------------------

        #region Get Data of All Mission




        public async Task<JsonResult> GetFilter()
        {
            var countries = _repo.GetCountry();
            var skills = _repo.GetSkill();
            var themes= _repo.GetTheme();
            var cities= _repo.GetCity();

            CreateMissionResponseModel model = new CreateMissionResponseModel()
            {
                Countries= countries,
                Skills = skills,
                Themes = themes,
                Cities = cities,

            };

            return new JsonResult(new Response<CreateMissionResponseModel>
            {
                Data = model,
                IsSuccess = true,
                Message = "",
                StatusCode = StatusCodes.Status200OK,
            });
        }




        /// <summary>
        /// Get Data of all Mission from repo and pass response to controller
        /// </summary>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="theme"></param>
        /// <param name="skill"></param>
        /// <param name="searchTerm"></param>
        /// <returns>JsonResult with data of all mission</returns>

        public async Task<JsonResult> GetAllMissions(MissionFilter missionFilter)
        {
            var data = await _repo.GetAllMissions(missionFilter);
            if (data != null)
            {

                return new JsonResult(new Response<IEnumerable<GetMissionResponseModel>>
                {
                    Data = data,
                    IsSuccess = true,
                    Message = "Get All Missions Data.",
                    StatusCode = StatusCodes.Status200OK,
                });
            }

            return new JsonResult(new Response<string>
            {
                Data = null,
                IsSuccess = false,
                Message = "Not Get Data",
                StatusCode = StatusCodes.Status400BadRequest,
            });
        }

        public async Task<JsonResult> RatingService(int missionId, int ratingValue)
        {
              await _repo.Rating(missionId, ratingValue);
            return new JsonResult(new Response<string>
            {
                Data = null,
                IsSuccess = true,
                Message = "Rating Given Successfully",
                StatusCode = StatusCodes.Status200OK,
            });
        }

        public async Task<JsonResult> FavouriteService(int missionId, int userId)
        {
            await _repo.Favourite(missionId, userId );
            return new JsonResult(new Response<string>
            {
                Data = null,
                IsSuccess = true,
                Message = "Added to Favourite",
                StatusCode = StatusCodes.Status200OK,
            });
        }

        #endregion

        //-------------------------------------------------------------------------Create Mission--------------------------------------------------------------------------------------

        #region Create new Mission

        /// <summary>
        /// Get Form for create new mission
        /// </summary>
        /// <returns></returns>

        public async Task<JsonResult> GetNewMission()
        {
            try
            {
                var countries =  _repo.GetCountry();
                var themes =  _repo.GetTheme();
                var skills = _repo.GetSkill();
                CreateMissionResponseModel model = new()
                {
                    Countries = countries,
                    Themes = themes,
                    Skills = skills,
                };
                return new JsonResult(new Response<CreateMissionResponseModel>
                {
                    Data = model,
                    IsSuccess = true,
                    Message = "",
                    StatusCode =StatusCodes.Status200OK,
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string>
                {
                    IsSuccess = false,
                    Message = ex.Message.ToString(),
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }
        }

        /// <summary>
        /// Create new mission and check image, video and document extension and upload only one video, image and document
        /// </summary>
        /// <param name="createMissionRequestModel"></param>
        /// <returns></returns>

        public async Task<JsonResult> CreateMission(CreateMissionRequestModel model)
        {
            using (var transaction = _repo.BeginTransaction())
                try
                {
                    Mission mission = mapper.Map<Mission>(model);
                    
                    MissionMedia missionMedia = new();
                    // Check if exactly 2 files are uploaded
                    if (model.Images!.Count < 1)
                    {
                        return new JsonResult(new Response<string>
                        {
                            IsSuccess = false,
                            Message = "Please upload one image and one video",
                            StatusCode = StatusCodes.Status400BadRequest,
                        });
                    }
                    else
                    {
                        bool hasImage = false;
                        //bool hasVideo = false;

                        foreach (var file in model.Images)
                        {
                            string extension = Path.GetExtension(file.FileName).ToLower();

                            if (ImageExtensions.Contains(extension))
                            {
                                hasImage = true;
                            }
                            //else if (VideoExtensions.Contains(extension))
                            //{
                            //    hasVideo = true;
                            //}
                            else
                            {
                                return new JsonResult(new Response<string>
                                {
                                    IsSuccess = false,
                                    Message = "Invalid file type. Please upload only image and video files.",
                                    StatusCode =StatusCodes.Status400BadRequest,
                                });
                            }
                        }

                        if (!hasImage )
                        {
                            return new JsonResult(new Response<string>
                            {
                                IsSuccess = false,
                                Message = "Please upload one image and one video",
                                StatusCode = StatusCodes.Status400BadRequest,
                            });
                        }
                        else
                        {
                            foreach (var file in model.Images)
                            {
                                string extension = Path.GetExtension(file.FileName).ToLower();

                                if (ImageExtensions.Contains(extension))
                                {
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await file.CopyToAsync(memoryStream);
                                        missionMedia.Image = memoryStream.ToArray();
                                    }
                                }
                                else if (VideoExtensions.Contains(extension))
                                {
                                    using (var memoryStream = new MemoryStream())
                                    {
                                        await file.CopyToAsync(memoryStream);
                                        mission.MissionVideo = memoryStream.ToArray();
                                    }
                                }
                                else
                                {
                                    return new JsonResult(new Response<string>
                                    {
                                        IsSuccess = false,
                                        Message = "Invalid file type. Please upload only image and video files.",
                                        StatusCode = StatusCodes.Status400BadRequest,
                                    });
                                }
                            }
                        }
                        await _repo.CreateMission(mission);
                        missionMedia.MissionId = mission.MissionId;

                        if (model.MissionSkill.Any())
                        {
                            List<MissionSkill> missionSkills = new();
                            foreach (var skill in model.MissionSkill)
                            {
                                MissionSkill missionSkill = new()
                                {
                                    MissionId = mission.MissionId,
                                    SkillId = skill
                                };
                                missionSkills.Add(missionSkill);
                            }
                            await _repo.AddMissionSkills(missionSkills);
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            await model.Document![0].CopyToAsync(memoryStream);
                            missionMedia.Document = memoryStream.ToArray();
                        }
                        await _repo.AddMissionMedia(missionMedia);
                        transaction.Commit();
                        return new JsonResult(new Response<string>
                        {
                            IsSuccess = true,
                            Message = "Mission created successfully",
                            StatusCode = StatusCodes.Status200OK,
                        });
                    }


                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return new JsonResult(new Response<string>
                    {
                        IsSuccess = false,
                        Message = ex.Message.ToString(),
                        StatusCode = StatusCodes.Status500InternalServerError,
                    });
                }
        }

        #endregion
    }
}
