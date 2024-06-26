using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using CI_Platform.Services.Implementation;
using CI_Platform.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CI_Platform_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly ILandingPageService _landingPageService;
        //protected Response response;
        public MissionController(ILandingPageService landingPageService)
        {
            _landingPageService = landingPageService;
        }

        [HttpGet("city")]
        public async Task<IActionResult> GetCityById([FromQuery]Int64 id)
        {
            return await _landingPageService.GetCityById(id);
        }

        [HttpGet("mission-filter")]
        public async Task<IActionResult> GetFilter()
        {
            return await _landingPageService.GetFilter();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMission([FromQuery] MissionFilter missionFilter)
        {
            return await _landingPageService.GetAllMissions(missionFilter);
        }

        [HttpPut("rating")]
        public async Task<IActionResult> MissionRating([FromQuery] int missionId, int ratingValue)
        {
            return await _landingPageService.RatingService(missionId, ratingValue);
        } 
        [HttpPost("favourite")]
        public async Task<IActionResult> MissionFavourite([FromQuery] int missionId, int userId )
        {
            return await _landingPageService.FavouriteService(missionId, userId);
        }
        /// <summary>
        /// Get creat Mission Form 
        /// </summary>
        /// <returns></returns>
        [HttpGet("AddMission")]
        public async Task<IActionResult> NewMission()
        {
            return await _landingPageService.GetNewMission();

        }

        /// <summary>
        /// Creat New Mission Post Method (On Submit Form)
        /// </summary>
        /// <param name="createMissionRequestModel"></param>
        /// <returns></returns>
        [HttpPost("AddMission")]
        public async Task<IActionResult> CreateNewMission([FromForm] CreateMissionRequestModel createMissionRequestModel)
        {
            return await _landingPageService.CreateMission(createMissionRequestModel);
        }
    }
}
