using CI_Platform.Models;
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interface
{
    public interface ILandingPageRepo
    {
        IDbContextTransaction BeginTransaction();
        //---------------------------Common DB --------------------------------
        List<DropdownResponseModel> GetCityById(Int64 id);
        List<DropdownResponseModel> GetCountry();

        List<DropdownResponseModel> GetTheme();

        List<DropdownResponseModel> GetSkill();
        List<DropdownResponseModel> GetCity();


        //-------------------------------------Get Mission----------------------------
        
        Task<IEnumerable<GetMissionResponseModel>> GetAllMissions(MissionFilter missionFilter);

        //Task<GetMissionResponseModel> GetMissionDetailRepo(int missionId);
        Task Rating(int missionId, int ratingValue);

        Task Favourite(int missionId, int userId);
        //-------------------------------------Create Mission----------------------------

        //Task<JsonResult> GetNewMission();
        
        Task CreateMission(Mission mission);
        Task AddMissionMedia(MissionMedia missionMedia);
        Task AddMissionSkills(List<MissionSkill> missionSkills);
    }
}
