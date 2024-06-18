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
    public interface ILandingPageRepo
    {

        //---------------------------Common DB --------------------------------
        List<DropdownResponseModel> GetCityById(Int64 id);
        List<DropdownResponseModel> GetCountry();

        //List<DropdownResponseModel> GetTheme();

        List<DropdownResponseModel> GetSkill();


        //-------------------------------------Get Mission----------------------------

        Task<IEnumerable<GetMissionResponseModel>> GetAllMissions(long country, long city, string theme, string skill, string searchTerm, int sortingOption);


        //-------------------------------------Create Mission----------------------------

        Task<JsonResult> GetNewMission();
        
        Task CreateMission(Mission mission);
        Task AddMissionMedia(MissionMedia missionMedia);

    }
}
