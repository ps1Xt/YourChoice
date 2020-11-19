using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using YourChoice.Api.Dtos.User;
using YourChoice.Api.Infrastructure.Configurations;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AuthOptions _authenticationOptions;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> userManager;

        public AccountController(IOptions<AuthOptions> authenticationOptions, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _authenticationOptions = authenticationOptions.Value;
            _signInManager = signInManager;
            this.userManager = userManager;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] UserLoginDto user)
        {
            
            var checkingPasswordResult = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);

            if (checkingPasswordResult.Succeeded)
            {
                var userId = Convert.ToString((await userManager.FindByNameAsync(user.Username)).Id);

                var ClaimId = new Claim("Id", userId);

                var ClaimUsername = new Claim("Username", user.Username);

                var Claims = new List<Claim> { ClaimId, ClaimUsername };

                var signinCredentials = new SigningCredentials(_authenticationOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256);
                
                var jwtSecurityToken = new JwtSecurityToken(
                     issuer: _authenticationOptions.Issuer,
                     audience: _authenticationOptions.Audience,
                     claims: Claims,
                     expires: DateTime.Now.AddDays(30),
                     signingCredentials: signinCredentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();

                var encodedToken = tokenHandler.WriteToken(jwtSecurityToken);

                return Ok(new { AccessToken = encodedToken });
            }

            return Unauthorized();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            if(userDto.Password != userDto.ConfirmPassword)
            {
                return BadRequest("Passwords must match");
            }

            var userExist = await userManager.FindByNameAsync(userDto.Username);

            if (userExist != null)
            {
                return BadRequest("User with this name already exists");
            }

            User user = new User()
            {
                UserName = userDto.Username
            };

            var result = await userManager.CreateAsync(user, userDto.Password);

            if (!result.Succeeded)
            {
                return BadRequest("Something wrong");
            }

            return Ok("Success");
        }
    }
}
