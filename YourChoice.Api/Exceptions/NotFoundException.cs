using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YourChoice.Api.Exceptions;

namespace YourChoice.Exceptions
{
    public class NotFoundException : ApiExeption
    {
        public NotFoundException(string message) : base(HttpStatusCode.NotFound, message)
        {

        }
    }
}
