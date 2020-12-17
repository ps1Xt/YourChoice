using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.User;
using YourChoice.Api.Exceptions;
using YourChoice.Api.Infrastructure.Configurations;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Services.implementation
{
    public class AccountService : IAccountService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly AuthOptions authenticationOptions;


        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, IOptions<AuthOptions> authenticationOptions)
        {
            this.authenticationOptions = authenticationOptions.Value;
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<SignInResult> CheckUserPassword(string userName, string password)
        {
            return await signInManager.PasswordSignInAsync(userName, password, false, false);
        }

        public async Task<string> GenerateJwt(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var ClaimUserName = new Claim(ClaimsIdentity.DefaultNameClaimType, userName);

            var Claims = new List<Claim>() { ClaimUserName };

            var signinCredentials = new SigningCredentials(authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                 issuer: authenticationOptions.Issuer,
                 audience: authenticationOptions.Audience,
                 claims: Claims,
                 expires: DateTime.Now.AddDays(30),
                 signingCredentials: signinCredentials
                 );

            var tokenHandler = new JwtSecurityTokenHandler();

            var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

            return encodedToken;
        }

        public async Task<IdentityResult> Register(RegisterUserDto userDto)
        {
            if (userDto.Password != userDto.ConfirmPassword)
            {
                throw new BadRequestException("Passwords must match");
            }

            var userExist = await userManager.FindByNameAsync(userDto.UserName);

            if (userExist != null)
            {
                throw new BadRequestException("User with this name already exists");
            }

            User user = new User()
            {
                UserName = userDto.UserName
            };

            var result = await userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                throw new BadRequestException("Something went wrong");
            }


            return result;
        }
    }
}
