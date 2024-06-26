using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ResponseModel
{
    public class CreateMissionResponseModel
    {
        public List<DropdownResponseModel> Cities { get; set; }
        public List<DropdownResponseModel>? Countries {  get; set; }
        public List<DropdownResponseModel>? Skills {  get; set; }
        public List<DropdownResponseModel>? Themes {  get; set; }
    }
}
