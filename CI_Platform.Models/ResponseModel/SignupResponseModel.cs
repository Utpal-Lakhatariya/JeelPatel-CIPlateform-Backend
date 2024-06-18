using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ResponseModel
{
    public class SignupResponseModel
    {
        public User? User { get; set; }
        public string? Message {  get; set; }
    }
}
