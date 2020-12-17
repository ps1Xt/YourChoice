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
        private readonly IPostService postService;


        private readonly INotificationService notificationService;

        public PostController(IPostService postService, INotificationService notificationService)
        {
            this.postService = postService;
            this.notificationService = notificationService;
        }
        [HttpGet("{id}")]
        [ApiExceptionFilter]
        [AllowAnonymous]
        public async Task<IActionResult> GetPost(int id)
        {
            var userName = User.Identity.Name;
            
            var post = await postService.GetPost(id,userName);

            return Ok(post);
        }
        [HttpPost("PaginatedSearch")]
        public async Task<IActionResult> GetPagedPosts([FromBody] PagedRequest pagedRequest)
        {
            var posts = await postService.GetPage(pagedRequest);
            return Ok(posts);
        }

        [HttpPost("MainPage/Home")]
        [AllowAnonymous]
        public async Task<IActionResult> GetMainPageHome(MainPageRequest mainPageRequest)
        {
            var posts = await postService.GetMainPageHome(mainPageRequest);
            return Ok(posts);
        }
        [HttpPost("MainPage/Subscriptions")]
        public async Task<IActionResult> GetMainPageSubscriptions(MainPageRequest mainPageRequest)
        {
            var userName = User.Identity.Name;

            var posts = await postService.GetMainPageSubscriptions(mainPageRequest, userName);
            return Ok(posts);
        }
        [HttpPost("MainPage/Favorites")]
        public async Task<IActionResult> GetMainPageFavorites(MainPageRequest mainPageRequest)
        {
            var userName = User.Identity.Name;
            var posts = await postService.GetMainPageFavorites(mainPageRequest, userName);
            return Ok(posts);
        }
        [HttpPost("MainPage/MyPosts")]
        public async Task<IActionResult> GetMainPageMyPosts(MainPageRequest mainPageRequest)
        {
            var userName = User.Identity.Name;
            var posts = await postService.GetMainPageMyPosts(mainPageRequest, userName);
            return Ok(posts);
        }

        [HttpPost]
        [ApiExceptionFilter]
        public async Task<IActionResult> CreatePost()
        {
            string userName = User.Identity.Name;

            Post post = await postService.CreatePost(Request.Form, userName);

            await notificationService.SubscribersNotify(userName);

            return Ok(new { id = post.Id });
        }


    }

}
