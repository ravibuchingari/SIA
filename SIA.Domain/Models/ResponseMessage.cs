using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIA.Domain.Models
{
    public class ResponseMessage(bool isSuccess, string message = "", object? data = null)
    {
        public bool IsSuccess { get; } = isSuccess;
        public string Message { get; } = message;
        public object? Data { get; set; } = data;
    }
}
