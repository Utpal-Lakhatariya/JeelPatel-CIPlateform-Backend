﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{

    [Table("User")]
    public class User
    {
        [Key]
        [MaxLength(20)]
        public Int64 UserId { get; set; }

        [MaxLength(16)]
        public string? FirstName { get; set; }

        [MaxLength(16)]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(128)]
        public string? Email { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Password { get; set; }

        [Required]
        [MaxLength(12)]
        public string? PhoneNumber { get; set; }

        [MaxLength(2048)]
        public string? Avatar { get; set; }


        public string? Skills { get; set; }

        [Column(TypeName = "text")]
        public string? WhyIVolunteer { get; set; }

        [MaxLength(16)]
        public string? EmployeeId { get; set; }

        [MaxLength(16)]
        public string? Department { get; set; }


        [MaxLength(20)]
        [ForeignKey("City")]
        public Int64 CityId { get; set; }

        [MaxLength(20)]
        [ForeignKey("Country")]
        public Int64 CountryId { get; set; }

        [Column(TypeName = "text")]
        public string? ProfileText { get; set; }

        [MaxLength(255)]
        public string? LinkedInUrl { get; set; }

        [MaxLength(255)]
        public string? Title { get; set; }

        public StatusEnum? Status { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }

        public City? City { get; set; }

        public Country? Country { get; set; }

        public ICollection<Comment>? Comments { get; set; }

        public ICollection<MissionApplication>? MissionApplications { get; set; }
        public ICollection<RecentVolunteer>? RecentVolunteers { get; set; }

        public ICollection<Story>? Stories { get; set; }
        public ICollection<UserMission>? UserMissions { get; set; }


    }
    public enum StatusEnum
    {
        Active = 0,
        NotActive = 1
    }
}
