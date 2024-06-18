using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.RequestModel
{
    public class CreateMissionRequestModel
    {

        [Required]
        public Int64? CountryId { get; set; }

        [Required]
        public Int64? CityId { get; set; }

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

        [Required]
        public IFormFile[]? Images { get; set; }

        [Required]
        public IFormFile? Document { get; set; }

        public int? MissionAvailability { get; set; }
    }
}


