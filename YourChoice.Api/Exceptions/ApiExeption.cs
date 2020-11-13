using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace YourChoice.Api.Exceptions
{
    public class ApiExeption : Exception
    {
        public HttpStatusCode Code { get; }

        public ApiExeption(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
