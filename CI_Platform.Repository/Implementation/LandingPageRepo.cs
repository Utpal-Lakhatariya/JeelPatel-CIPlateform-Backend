using AutoMapper;
using CI_Platform.Models;
using CI_Platform.Models.DBContext;
using CI_Platform.Models.RequestModel;
using CI_Platform.Models.ResponseModel;
using CI_Platform.Repository.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static CI_Platform.Models.ResponseModel.GetNewMissionResponseModel;

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
            _connectionString = configuration.GetConnectionString("DefaultConnection");
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

        public async Task<IEnumerable<GetMissionResponseModel>> GetAllMissions(long country, long city, string theme, string skill, string searchTerm, int sortingOption)
        {

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();

                var missionData = await connection.QueryAsync<GetMissionResponseModel>
                    (
                        "SELECT * FROM GetMissions(@p_country, @p_city, @p_theme, @p_skill, @p_searchTerm, @p_sortingOption)",
                        new
                        {
                            p_country = country,
                            p_city = city,
                            p_theme = theme,
                            p_skill = skill,
                            p_searchTerm = searchTerm,
                            p_sortingOption = sortingOption
                        }
                    );
                    return missionData;
            }

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


        public Task<JsonResult> GetNewMission()
        {
            CreateMissionResponseModel createMissionResponseModel = new CreateMissionResponseModel()
            {
                Country = GetCountry(),
                Skill = GetSkill(),
                Theme = GetTheme(),

            };
            if (createMissionResponseModel != null)
            {
                return Task.FromResult(new JsonResult(new Response<CreateMissionResponseModel>()
                {
                    Data = createMissionResponseModel,
                    Message = "",
                    IsSuccess = true,
                    StatusCode = 200,
                }));
            }
            return Task.FromResult(new JsonResult(new Response<CreateMissionResponseModel>()
            {
                Data = null,
                Message = "",
                IsSuccess = false,
                StatusCode = 404,
            }));

        }


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

        #endregion

    }
}
