using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.RequestModel
{
    public class CountryRequestModel
    {
        public int CountryId { get; set; }

        [Required]
        public string? CountryName { get; set; }

    }
}
