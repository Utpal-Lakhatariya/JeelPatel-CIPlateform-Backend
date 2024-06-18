using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.RequestModel
{
    public class SignupRequestModel
    {
        [StringLength(16, ErrorMessage = "Only 16 Characaters are Accepted")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "FirstName Accepts Only Text Characters")]
        public string? FirstName { get; set; }

        [StringLength(16, ErrorMessage = "Only 16 Characaters are Accepted")]
        [RegularExpression(@"^[a-zA-Z]*$", ErrorMessage = "LastName Accepts Only Text Characters")]
        public string? LastName { get; set; }

        [Required]
        [MaxLength(128)]
        [RegularExpression(@"^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$", ErrorMessage = "Invalid Email")]
        public string? Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Minimum eight characters and at least one letter, one number and one special character is mandatory")]
        public string? Password { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Phone Number should be of 10 Numbers")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Please Enter Valid Phone Number")]
        public string? PhoneNumber { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = "Minimum eight characters and at least one letter, one number and one special character is mandatory")]

        [Compare("Password")]
        public string? ConfirmPassword { get; set; }

        
    }
}
