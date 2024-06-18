using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Models.ResponseModel
{
    public class Response<T>
    {
        public T? Data { get; set; }
        public string? Message {  get; set; }
         public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
}
