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
    public class FavoritesController : ControllerBase
    {
        private readonly IFavoriteService favoriteService;
        private readonly INotificationService notificationService;

        public FavoritesController(IFavoriteService favoriteService, INotificationService notificationService)
        {
            this.favoriteService = favoriteService;
            this.notificationService = notificationService;
        }

        [HttpPut]
        public async Task<IActionResult> AddToFavorites(PostIdDto postIdDto)
        {
            var userName = User.Identity.Name;

            var postId = postIdDto.Id;

            await favoriteService.AddToFavorites(postId, userName);

            await notificationService.FavoritesNotify(postId, userName);

            return Ok(new { Result = true });

        }

        [HttpPatch]
        public async Task<IActionResult> RemoveFromFavorites(PostIdDto postIdDto)
        {
            var userName = User.Identity.Name;

            var postId = postIdDto.Id;

            await favoriteService.RemoveFromFavorites(postId, userName);

            return Ok(new { Result = true });
        }

    }
}
