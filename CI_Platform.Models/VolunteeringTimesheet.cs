using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    [Table("VolunteeringTimesheet")]
    public class VolunteeringTimesheet
    {
        [Key]
        [MaxLength(20)]
        public Int64 VolunteeringId { get; set; }

        [Required]
        [ForeignKey("Mission")]
        public int MissionId { get; set; }

        [Required]
        [MaxLength(128)]
        public string? MissionTitle { get; set; }

        [Required]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        [Required]
        public TimeSpan Hours { get; set; }

        [Required]
        public TimeSpan Minutes { get; set; }

        public int Action { get; set; }

        public Mission? Mission { get; set; }
    }
}
