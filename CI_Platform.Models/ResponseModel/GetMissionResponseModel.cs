using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ResponseModel
{
    public class GetMissionResponseModel
    {
       
          
            public string? MissionTitle { get; set; }
            public string? MissionShortDescription { get; set; }
            public string? MissionDescription { get; set; }
           
            public string? Country { get; set; }
            public string? City { get; set; }
            public string? MissionOrganisationName { get; set; }
            public string? MissionOrganisationDetail { get; set; }
            public DateTime MissionStartDate { get; set; }
            public DateTime MissionEndDate { get; set; }
            public int MissionType { get; set; }
            public int TotalSeats { get; set; }
            public int MissionRating { get; set; }
            public int MissionRatingCount { get; set; }
            public DateTime MissionRegistrationDeadline { get; set; }
            public string? MissionTheme { get; set; }
            public string? MissionSkills { get; set; }
            public int MissionAvailability { get; set; }
       
            public byte[]? Image { get; set; }
           
        

    }
}
