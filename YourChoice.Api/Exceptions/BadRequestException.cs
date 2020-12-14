using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace YourChoice.Api.Exceptions
{
    public class BadRequestException : ApiExeption
    {
        public BadRequestException(string message) : base(HttpStatusCode.BadRequest, message)
        {

        }
    }
}
