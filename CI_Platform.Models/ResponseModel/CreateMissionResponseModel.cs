using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ResponseModel
{
    public class CreateMissionResponseModel
    {
       // public List<DropdownResponseModel>City {  get; set; }
        public List<DropdownResponseModel>? Country {  get; set; }
        public List<DropdownResponseModel>? Skill {  get; set; }
        public List<DropdownResponseModel>? Theme {  get; set; }
    }
}
