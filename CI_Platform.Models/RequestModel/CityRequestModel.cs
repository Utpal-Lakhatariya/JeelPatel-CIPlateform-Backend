using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.RequestModel
{
    public class CityRequestModel
    {
       
        public int CityId { get; set; }

        [Required]
        public string? Country { get; set; }

        [Required]
        public string? CityName { get; set; }

    }
}
