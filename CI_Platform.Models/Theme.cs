using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models
{
    [Table("Theme")]
    public class Theme
    {
        [Key]
        [Required]
        public int ThemeId {  get; set; }

        [Required]
        [MaxLength(50)]
        public string theme {  get; set; }
        [Required]
        public bool Status { get; set; }


    }
}
