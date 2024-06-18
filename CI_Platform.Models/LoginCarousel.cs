using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    [Table("LoginCarousel")]
    public class LoginCarousel
    {
        [Key]
        [MaxLength(10)]
        public int CarouselId { get; set; }

        [Required]
        public byte[] CarouselImage { get; set; } =null!;

        [MaxLength(255)]
        public string? CarouselHead { get; set; }

        [MaxLength(255)]
        public string? CarouselText { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}
