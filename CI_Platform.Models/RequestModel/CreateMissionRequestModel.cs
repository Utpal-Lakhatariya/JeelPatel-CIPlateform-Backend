﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.RequestModel
{
    [DateComparison("RegistrationDeadline", "MissionStartDate", ErrorMessage = "'Mission Start Date' must be greater than or equal to 'Registration Deadline'.")]
    [DateComparison("MissionStartDate", "MissionEndDate", ErrorMessage = "'Mission End Date' must be greater than or equal to 'Mission Start Date'.")]
    public class CreateMissionRequestModel
    {
        [Required]
        public Int64 CountryId { get; set; }

        [Required]
        public Int64 CityId { get; set; }

        [Required]
        [MaxLength(128)]

        public string MissionTitle { get; set; } = null!;

        [Required]
        [MaxLength(256)]
        public string MissionShortDescription { get; set; } = null!;

        [Required]
        [MaxLength(2048)]
        public string MissionDescription { get; set; } = null!;

        [Required]
        [MaxLength(50)]
        public string MissionOrganisationName { get; set; } = null!;

        [Required]
        [MaxLength(2048)]
        public string MissionOrganisationDetail { get; set; } = null!;

        [Required]
        public int MissionType { get; set; }

        [RequiredIfMissionType(1)]
        public DateOnly? MissionStartDate { get; set; }

        [RequiredIfMissionType(1)]
        public DateOnly? MissionEndDate { get; set; }

        [Required]
        [Range(0, Int32.MaxValue)]
        public int TotalSeats { get; set; }

        [RequiredIfMissionType(1)]
        [Range(0, Int32.MaxValue)]
        public int TotalGoal { get; set; }

        [RequiredIfMissionType(2)]
        public string? GoalObject { get; set; }

        [RequiredIfMissionType(1)]
        public DateOnly? MissionRegistrationDeadline { get; set; }

        [Required]
        public int ThemeId { get; set; }

        [Required]
        public List<int> MissionSkill { get; set; } = new List<int>();

        public List<IFormFile>? Images { get; set; }

        public List<IFormFile>? Document { get; set; }

        [Required]
        public int MissionAvailability { get; set; }
    }

    public class RequiredIfMissionTypeAttribute : ValidationAttribute
    {
        private readonly int _missionType;

        public RequiredIfMissionTypeAttribute(int missionType)
        {
            _missionType = missionType;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var missionTypeProperty = validationContext.ObjectType.GetProperty("MissionType");
            if (missionTypeProperty == null)
            {
                return new ValidationResult($"MissionType property not found on {validationContext.ObjectType.FullName}");
            }
            var mission = (CreateMissionRequestModel)validationContext.ObjectInstance;
            var missionTypeValue = mission.MissionType;
            if (missionTypeValue == _missionType && value == null)
            {
                return new ValidationResult($"{validationContext.DisplayName} is required when MissionType is {_missionType}");
            }

            return ValidationResult.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class DateComparisonAttribute : ValidationAttribute
    {
        private readonly string _startDateProperty;
        private readonly string _endDateProperty;

        public DateComparisonAttribute(string startDateProperty, string endDateProperty)
        {
            _startDateProperty = startDateProperty;
            _endDateProperty = endDateProperty;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var startDateProperty = validationContext.ObjectType.GetProperty(_startDateProperty);
            var endDateProperty = validationContext.ObjectType.GetProperty(_endDateProperty);

            if (startDateProperty == null || endDateProperty == null)
            {
                return ValidationResult.Success;
            }

            var startDateValue = (DateOnly?)startDateProperty.GetValue(validationContext.ObjectInstance);
            var endDateValue = (DateOnly?)endDateProperty.GetValue(validationContext.ObjectInstance);

            if (startDateValue.HasValue && endDateValue.HasValue && startDateValue > endDateValue)
            {
                return new ValidationResult($"'{_endDateProperty}' must be greater than or equal to '{_startDateProperty}'.");
            }

            return ValidationResult.Success;
        }
    }
}


