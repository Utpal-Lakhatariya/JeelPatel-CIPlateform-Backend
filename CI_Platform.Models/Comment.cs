using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Required]
        [MaxLength(50)]
        public int CommentId {  get; set; }

        [Column("MissionTitle")]
        [Required]
        [MaxLength(50)]
        public string? MissionName { get; set; }

        [Required]
        [MaxLength(20)]
        [ForeignKey("User")]
        public Int64 UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string? UserName { get; set; }

        [MaxLength(256)]
        public string? Comments { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedDate { get; set; }

        public User? User { get; set; }
    }
}
