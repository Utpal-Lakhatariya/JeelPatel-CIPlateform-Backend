using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    [Table("Story")]
    public class Story
    {
        [Key]
        [Required]
        public int StoryId { get; set; }
        [Required]
        [Column("StoryTitle")]
        public string StoryTitle { get; set;}
        [Required]
        [Column("MissionTitle")]
        public string MissionTitle {  get; set;}

        [Required]
        [ForeignKey("User")]
        public Int64 UserId {  get; set; }
        [Required]
        public string? StoryDescription { get; set; }

        public bool? Status {  get; set; }
        public User? User { get; set; }
    }
}
