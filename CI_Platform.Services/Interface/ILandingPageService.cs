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
        Task<JsonResult> GetAllMissions(long country, long city, string theme, string skill, string searchTerm, int sortingOption);

        //-------------------------------------Create Mission----------------------------
        Task<JsonResult> GetNewMission();
        Task<JsonResult> CreateMission(CreateMissionRequestModel createMissionRequestModel);

    }
}
