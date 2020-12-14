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

        public RatingController(IRatingService ratingService)
        {
            this.ratingService = ratingService;
        }

        [HttpPut]
        public async Task<IActionResult> Put(PostRatingDto postRatingDto)
        {
            var userName = User.Identity.Name;

            var avgRating = await ratingService.RatePost(postRatingDto, userName);

            return Ok(new { AvgRating = avgRating });
        }


    }
}
