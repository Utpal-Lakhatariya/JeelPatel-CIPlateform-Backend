using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Services.Interface
{
    public interface IJWTService
    {
        public Task<string> GenerateJWTToken(User user);

    }
}
