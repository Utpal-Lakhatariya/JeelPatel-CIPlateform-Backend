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
using System.Linq;
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


        #region

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
                return Task.FromResult(new JsonResult(new Response<List<DropdownResponseModel>>
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


        //-------------------------------------------------------------------------Get Mission--------------------------------------------------------------------------------------

        #region Get Data of All Mission

        /// <summary>
        /// Get Data of all Mission from repo and pass response to controller
        /// </summary>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="theme"></param>
        /// <param name="skill"></param>
        /// <param name="searchTerm"></param>
        /// <returns>JsonResult with data of all mission</returns>

        public async Task<JsonResult> GetAllMissions(long country, long city, string theme, string skill, string searchTerm, int sortingOption)
        {
            var data = await _repo.GetAllMissions(country, city, theme, skill, searchTerm, sortingOption);
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

        #endregion


        #region Create new Mission

        /// <summary>
        /// Get Form for create new mission
        /// </summary>
        /// <returns></returns>

        public async Task<JsonResult> GetNewMission()
        {
            return await _repo.GetNewMission();
        }

        /// <summary>
        /// Create new mission and check image, video and document extension and upload only one video, image and document
        /// </summary>
        /// <param name="createMissionRequestModel"></param>
        /// <returns></returns>

        public async Task<JsonResult> CreateMission(CreateMissionRequestModel createMissionRequestModel)
        {
            try
            {
                Mission mission = mapper.Map<Mission>(createMissionRequestModel);
                MissionMedia missionMedia = new();
                // Check if exactly 2 files are uploaded
                if (createMissionRequestModel.Images.Length != 2)
                {
                    return new JsonResult(new Response<string>
                    {
                        Data = null,
                        IsSuccess = false,
                        Message = "Please upload one image and one video",
                        StatusCode = StatusCodes.Status400BadRequest,
                    });
                }
                else
                {
                    bool hasImage = false;
                    bool hasVideo = false;

                    foreach (var file in createMissionRequestModel.Images)
                    {
                        string extension = Path.GetExtension(file.FileName).ToLower();

                        if (ImageExtensions.Contains(extension))
                        {
                            hasImage = true;
                        }
                        else if (VideoExtensions.Contains(extension))
                        {
                            hasVideo = true;
                        }
                        else
                        {
                            return new JsonResult(new Response<string>
                            {
                                Data = null,
                                IsSuccess = false,
                                Message = "Invalid file type. Please upload only image and video files.",
                                StatusCode = StatusCodes.Status400BadRequest,
                            });
                        }
                    }

                    if (!hasImage || !hasVideo)
                    {
                        return new JsonResult(new Response<string>
                        {
                            Data = null,
                            IsSuccess = false,
                            Message = "Please upload one image and one video",
                            StatusCode = StatusCodes.Status400BadRequest,
                        });
                    }
                    else
                    {
                        foreach (var file in createMissionRequestModel.Images)
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
                                    Data = null,
                                    IsSuccess = false,
                                    Message = "Invalid file type. Please upload only image and video files.",
                                    StatusCode = StatusCodes.Status400BadRequest,
                                });
                            }
                        }
                    }
                    await _repo.CreateMission(mission);
                    missionMedia.MissionId = mission.MissionId;
                    using (var memoryStream = new MemoryStream())
                    {
                        await createMissionRequestModel.Document.CopyToAsync(memoryStream);
                        missionMedia.Document = memoryStream.ToArray();
                    }
                    await _repo.AddMissionMedia(missionMedia);

                    return new JsonResult(new Response<string>
                    {
                        Data = null,
                        IsSuccess = true,
                        Message = "Mission created successfully",
                        StatusCode = StatusCodes.Status200OK
                    });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new Response<string>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "Internal server error: " + ex.Message,
                    StatusCode = StatusCodes.Status500InternalServerError,
                });
            }

        }

        #endregion
    }
}
