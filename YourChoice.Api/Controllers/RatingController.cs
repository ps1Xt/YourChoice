using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions;
using YourChoice.Api.Services.interfaces;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatingController : ControllerBase
    {
        private readonly IRatingService ratingService;
        private readonly INotificationService notificationService;

        public RatingController(IRatingService ratingService, INotificationService notificationService)
        {
            this.ratingService = ratingService;
            this.notificationService = notificationService;
        }

        [HttpPut]
        public async Task<IActionResult> Put(PostRatingDto postRatingDto)
        {
            var userName = User.Identity.Name;

            var avgRating = await ratingService.RatePost(postRatingDto, userName);

            await notificationService.RatingNotify(postRatingDto, userName);

            return Ok(new { AvgRating = avgRating });
        }


    }
}
