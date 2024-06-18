using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ResponseModel
{
    public class GetNewMissionResponseModel
    {
        public List<MissionResponseSubModel> MissionResponseSub { get; set; }
        public List<MissionMediaResponseSubModel> MissionMediaResponseSub { get; set; }
    }
    public class MissionResponseSubModel
    {
        public string? Country { get; set; }

        public string? City { get; set; }

        [Required]
        public string? MissionTitle { get; set; }

        [Required]
        public string? MissionDescription { get; set; }

        [Required]
        public string? MissionOrganisationName { get; set; }

        [Required]
        public string? MissionOrganisationDetail { get; set; }

        [Required]
        public DateTime? MissionStartDate { get; set; }

        [Required]
        public DateTime? MissionEndDate { get; set; }

        public int? TotalSeats { get; set; }

        [Required]
        public DateTime? MissionRegistrationDeadline { get; set; }

        [Required]
        public string? MissionTheme { get; set; }

        public string? MissionSkills { get; set; }

        public int? MissionAvailability { get; set; }
    }

    public class MissionMediaResponseSubModel
    {
        [Required]
        public byte[]? Images { get; set; }

        [Required]
        public byte[]? Document { get; set; }
    }



}
