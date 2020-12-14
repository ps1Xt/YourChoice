using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Services.interfaces;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService subscriptionService;
        private readonly INotificationService notificationService;

        public SubscriptionController(ISubscriptionService subscriptionService, INotificationService notificationService)
        {
            this.subscriptionService = subscriptionService;
            this.notificationService = notificationService;
        }

        [HttpPut]
        public async Task<IActionResult> Subscribe(PostOwnerDto postOwnerDto)
        {

            var who = User.Identity.Name;

            var toWhom = postOwnerDto.UserName;

            var result = await subscriptionService.Subscribe(who, toWhom);

            if (result == true)
            {
                await notificationService.SubscriptionNotify(who, toWhom);

            }

            return NoContent();

        }

        [HttpPatch]
        public async Task<IActionResult> UnSubscribe(PostOwnerDto postOwnerDto)
        {

            var who = User.Identity.Name;

            var toWhom = postOwnerDto.UserName;

            await subscriptionService.UnSubscribe(who, toWhom);

            return NoContent();

        }

    }
}
