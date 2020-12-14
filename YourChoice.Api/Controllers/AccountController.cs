﻿using System;
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
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        private readonly INotificationService notificationService;

        public AccountController(IAccountService accountService, INotificationService notificationService)
        {
            this.accountService = accountService;
            this.notificationService = notificationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginDto user)
        {

            var checkingPasswordResult = await accountService.checkUserPassword(user.UserName, user.Password);

            if (checkingPasswordResult.Succeeded)
            {
                var token = await accountService.GenerateJwt(user.UserName);

                return Ok(new { AccessToken = token });
            }

            return Unauthorized();
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserDto userDto)
        {
            var result = await accountService.Register(userDto);

            return Ok(result.Succeeded);
        }
    }
}
