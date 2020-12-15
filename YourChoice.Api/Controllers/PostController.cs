using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using Imgur.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;
using System.Linq;
using YourChoice.Api.Infrastructure.Models;
using System.Diagnostics;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService service;

        private readonly IMapper mapper;

        private readonly UserManager<User> userManager;

        private readonly IPhotoService photoService;

        public PostController(IPostService service, IMapper mapper, UserManager<User> userManager, IPhotoService photoService)
        {
            this.service = service;
            this.mapper = mapper;
            this.userManager = userManager;
            this.photoService = photoService;
        }
        [HttpGet("{id}")]
        [ApiExceptionFilter]
        [AllowAnonymous]
        public async Task<IActionResult> GetPost(int id)
        {

            var post = await service.GetPost(id);

            var result = mapper.Map<FullPostDto>(post);

            result.isInFavorites = post.Favorites.SingleOrDefault(x => x.User.UserName == User.Identity.Name)?.Value ?? false;

            result.isSubscribed = post.User.Subscribers.SingleOrDefault(x => x.Who.UserName == User.Identity.Name)?.Value ?? false;

            return Ok(result);
        }
        [HttpPost("PaginatedSearch")]
        public async Task<IActionResult> GetPagedPosts([FromBody] PagedRequest pagedRequest)
        {
            var posts = await service.GetPage(pagedRequest);
            return Ok(posts);
        }

        [HttpPost("MainPage/Home")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMainPageHome(MainPageRequest mainPageRequest)
        {
            var posts = await service.GetMainPageHome(mainPageRequest);
            return Ok(posts);
        }
        [HttpPost("MainPage/Subscriptions")]
        public async Task<IActionResult> GetMainPageSubscriptions(MainPageRequest mainPageRequest)
        {
            var userName = User.Identity.Name;

            var posts = await service.GetMainPageSubscriptions(mainPageRequest, userName);
            return Ok(posts);
        }
        [HttpPost("MainPage/Favorites")]
        public async Task<IActionResult> GetMainPageFavorites(MainPageRequest mainPageRequest)
        {
            var userName = User.Identity.Name;
            var posts = await service.GetMainPageFavorites(mainPageRequest, userName);
            return Ok(posts);
        }
        [HttpPost("MainPage/MyPosts")]
        public async Task<IActionResult> GetMainPageMyPosts(MainPageRequest mainPageRequest)
        {
            var userName = User.Identity.Name;
            var posts = await service.GetMainPageMyPosts(mainPageRequest, userName);
            return Ok(posts);
        }

        [HttpPost]
        [ApiExceptionFilter]
        public async Task<IActionResult> CreatePost()
        {
            User user = await userManager.FindByNameAsync(User.Identity.Name);
            Post post = await service.CreatePost(Request.Form, user);

            var result = mapper.Map<FullPostDto>(post);

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, result);
        }


    }

}
