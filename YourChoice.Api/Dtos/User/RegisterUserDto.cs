using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Dtos.User
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

    }
}
