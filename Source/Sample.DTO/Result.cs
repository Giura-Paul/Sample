using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.DTO
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public static Result Success(string message) => new Result { IsSuccess = true, Message = message };
        public static Result Error(string message) => new Result { IsSuccess = false, Message = message };
    }
}
