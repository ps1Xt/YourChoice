using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.User;

namespace YourChoice.Api.Services.interfaces
{
    public interface IAccountService
    {
        public Task<SignInResult> CheckUserPassword(string userName, string password);

        public Task<string> GenerateJwt(string userName);

        public Task<IdentityResult> Register(RegisterUserDto userDto);
    }
}
