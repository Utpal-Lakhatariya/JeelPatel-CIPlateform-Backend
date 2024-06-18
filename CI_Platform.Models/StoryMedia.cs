using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    [Table("StoryMedia")]
    public class StoryMedia
    {
        [Key]
        [MaxLength(20)]
        public Int64 MediaId { get; set; }

        public byte[] Image { get; set; }
        public byte[] Document { get; set; }

        [Required]
        [ForeignKey("Mission")]
        public int MissionId { get; set; }

        public Mission Mission { get; set; }
    }
}
