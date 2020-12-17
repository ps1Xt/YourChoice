using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourChoice.Api.Dtos.Message;
using YourChoice.Api.Services.interfaces;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;
        private readonly IMapper mapper;

        public NotificationController(INotificationService notificationService, IMapper mapper)
        {
            this.notificationService = notificationService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessages()
        {
            var messages = await notificationService.GetMessages(User.Identity.Name);

            return Ok(messages);

        }


        [HttpPatch]
        public async Task<IActionResult> ReadMessages()
        {
            var userName = User.Identity.Name;

            var result = await notificationService.ReadMessages(userName);

            return Ok(new { Result = result });
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCountOfNewMessages()
        {
            var userName = User.Identity.Name;

            var number = await notificationService.GetCountOfUnreadMessages(userName);

            return Ok(new { Number = number });
        }


    }
}
