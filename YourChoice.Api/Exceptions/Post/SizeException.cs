using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using YourChoice.Api.Exceptions;

namespace YourChoice.Api.Exceptions.Post
{
    public class SizeException : ApiExeption
    {
        public SizeException(string message) : base(HttpStatusCode.BadRequest, message)
        {

        }
    }
}
