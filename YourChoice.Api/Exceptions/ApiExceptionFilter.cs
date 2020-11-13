﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Exceptions
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception is ApiExeption apiException)
            {
                context.Result = new ObjectResult(apiException.Message) { StatusCode = (int)apiException.Code };
            }
        }
    }
}
