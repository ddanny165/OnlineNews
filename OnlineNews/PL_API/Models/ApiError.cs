using System;
using System.Net;

namespace PL_API.Models
{
    public class ApiError
    {
        public string Message { get; set; }

        public string Status { get; set; }

        public ApiError(string message, HttpStatusCode code)
        {
            Message = message;
            Status = code.ToString();
        }
    }
}
