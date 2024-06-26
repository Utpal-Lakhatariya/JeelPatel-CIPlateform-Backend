using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    public class MissionType
    {
        [Key]
        [MaxLength(20)]
        public Int64 Id { get; set; }

        [Required]
        [ForeignKey("Mission")]
        public int MissionId { get; set; }

        [Required]
        public string Type { get; set; }

        public Mission Mission { get; set; }

    }
}
