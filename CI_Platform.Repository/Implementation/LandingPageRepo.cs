using AutoMapper;
using CI_Platform.Models;
using CI_Platform.Models.DBContext;
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using CI_Platform.Repository.Interface;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace CI_Platform.Repository.Implementation
{
    public class LandingPageRepo : ILandingPageRepo
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;
        private readonly string _connectionString;
        public LandingPageRepo(AppDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            this.mapper = mapper;
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        //----------------------------------------------Common DB Methods-----------------------------------------------

        #region Get City From Country, Country, Theme, Skill


        public List<DropdownResponseModel> GetCityById(Int64 id)
        {
            var city = _context.Cities.Where(i => i.CountryId == id).Select(r => new DropdownResponseModel()
            {
                Value = r.CityId,
                Name = r.CityName,
            }).ToList();
            return city;
        }

        public List<DropdownResponseModel> GetCountry()
        {
            var country = _context.Countries.Select(i => new DropdownResponseModel()
            {
                Value = i.CountryId,
                Name = i.CountryName
            }).ToList();
            return country;
        }
        public List<DropdownResponseModel> GetTheme()
        {
            var themes = _context.Themes.Select(r => new DropdownResponseModel()
            {
                Value = r.ThemeId,
                Name = r.theme,
            }).ToList();
            return themes;
        }

        public List<DropdownResponseModel> GetSkill()
        {
            var skills = _context.Skills.Select(i => new DropdownResponseModel()
            {
                Value = i.SkillId,
                Name = i.Skills
            }).ToList();
            return skills;
        }
        public List<DropdownResponseModel> GetCity()
        {
            var cities = _context.Cities.Select(i => new DropdownResponseModel()
            {
                Value = i.CityId,
                Name = i.CityName
            }).ToList();
            return cities;
        }


        #endregion

        //-------------------------------------Get Mission----------------------------

        #region Get All Mission Data with filtering

        /// <summary>
        /// Get All Mission Data by applying filtering
        /// </summary>
        /// <param name="country"></param>
        /// <param name="city"></param>
        /// <param name="theme"></param>
        /// <param name="skill"></param>
        /// <param name="searchTerm"></param>
        /// <returns>Mission Data</returns>

        public async Task<IEnumerable<GetMissionResponseModel>> GetAllMissions(MissionFilter missionFilter)
        {

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var missionData = await connection.QueryAsync<GetMissionResponseModel>
                    (
                        "SELECT * FROM GetMissionsData(@p_user_id, @p_country,  @p_city, @p_theme, @p_skill, @p_sortingOption, @p_searchTerm)",
                        new
                        {
                            p_country = missionFilter.Country,
                            p_city = missionFilter.Cities.Count == 0 ? null : missionFilter.Cities,
                            p_theme = missionFilter.Themes.Count == 0 ? null : missionFilter.Themes,
                            p_skill = missionFilter.Skills.Count == 0 ? null : missionFilter.Skills,
                            p_searchTerm = missionFilter.SearchValue,
                            p_sortingOption = missionFilter.SortingOption,
                            p_user_id = 2
                        }
                    );
                return missionData;
            }

        }

        //public async Task<GetMissionResponseModel> GetMissionDetailRepo(int missionId)
        //{
        //    using(var connection = new NpgsqlConnection(_connectionString))
        //            {
        //        connection.Open ();

        //        var missionDetails = await connection.QueryAsync<GetMissionResponseModel>
        //            (
        //            );
        //        return missionDetails;
        //    }
        //}

        public async Task Rating(int missionId, int ratingValue)
        {
            var mission = await _context.Missions.FirstOrDefaultAsync(i => i.MissionId == missionId);
            var userMission = await _context.UserMissions.FirstOrDefaultAsync(i => i.MissionId == missionId);
            if (mission == null) return;

            if (userMission == null)
            {
                userMission!.MissionId = missionId;
            }
            mission.MissionRating = ratingValue;
            userMission.Ratings = ratingValue;
            await _context.SaveChangesAsync();
        }


        public async Task Favourite(int missionId, int userId)
        {
            var userMission = await _context.UserMissions.FirstOrDefaultAsync(i => i.MissionId == missionId);
            if (userMission == null)
            {
                UserMission data = new UserMission
                {
                    UserId = userId,
                    MissionId = missionId,
                    Favourite = 1
                };
                await _context.UserMissions.AddAsync(data);

            }
            else
            {
                userMission.Favourite = userMission.Favourite == 0 ? 1 : 0;
            }

            await _context.SaveChangesAsync();
        }
        #endregion
        //-------------------------------------------Create New Mission------------------------------------------

        #region Create New Mission

        /// <summary>
        /// Function to Create New Mission Form
        /// </summary>
        /// <returns>
        /// It Will return list of country and skills
        /// </returns>


        //public Task<JsonResult> GetNewMission()
        //{
        //    CreateMissionResponseModel createMissionResponseModel = new CreateMissionResponseModel()
        //    {
        //        Country = GetCountry(),
        //        Skill = GetSkill(),
        //        Theme = GetTheme(),

        //    };
        //    if (createMissionResponseModel != null)
        //    {
        //        return Task.FromResult(new JsonResult(new Response<CreateMissionResponseModel>()
        //        {
        //            Data = createMissionResponseModel,
        //            Message = "",
        //            IsSuccess = true,
        //            StatusCode = 200,
        //        }));
        //    }
        //    return Task.FromResult(new JsonResult(new Response<CreateMissionResponseModel>()
        //    {
        //        Data = null,
        //        Message = "",
        //        IsSuccess = false,
        //        StatusCode = 404,
        //    }));

        //}


        /// <summary>
        /// Fucntion to Submit new Mission form
        /// </summary>
        /// <param name="request"></param>
        /// <returns> Return Data of Mission Table </returns>


        public async Task CreateMission(Mission mission)
        {
            try
            {
                await _context.Missions.AddAsync(mission);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task AddMissionMedia(MissionMedia missionMedia)
        {
            try
            {
                await _context.MissionMedias.AddAsync(missionMedia);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception();
            }
        }

        public async Task AddMissionSkills(List<MissionSkill> missionSkills)
        {
            try
            {
                await _context.AddRangeAsync(missionSkills);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

    }
}
