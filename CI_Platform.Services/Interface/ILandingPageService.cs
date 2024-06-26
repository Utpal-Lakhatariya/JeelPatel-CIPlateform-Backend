using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Interface
{
    public interface ILandingPageService
    {
        Task<JsonResult>GetCityById(Int64 id);

        //-------------------------------------Get Mission----------------------------
        Task<JsonResult> GetFilter();
        Task<JsonResult> GetAllMissions(MissionFilter missionFilter);

        Task<JsonResult> RatingService(int missionId, int ratingValue);
        Task<JsonResult> FavouriteService(int missionId, int userId );

        //-------------------------------------Create Mission----------------------------
        Task<JsonResult> GetNewMission();
        Task<JsonResult> CreateMission(CreateMissionRequestModel createMissionRequestModel);

    }
}
