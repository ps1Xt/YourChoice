﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Domain;
using YourChoice.Api.Dtos.Post;
using YourChoice.Domain.Auth;
using Microsoft.AspNetCore.Http;
using YourChoice.Api.Infrastructure.Models;

namespace YourChoice.Api.Services.interfaces
{
    public interface IPostService
    {

        public Task<Post> CreatePost(IFormCollection form, User user);

        public Task<PaginatedResult<PostGridRowDto>> GetPage(PagedRequest pagedRequest);

        public Task<PaginatedResult<PostCardDto>> GetMainPageHome(MainPageRequest pagedRequest);

        public Task<PaginatedResult<PostCardDto>> GetMainPageSubscriptions(MainPageRequest pagedRequest, string userName);

        public Task<PaginatedResult<PostCardDto>> GetMainPageFavorites(MainPageRequest pagedRequest, string userName);

        public Task<PaginatedResult<PostCardDto>> GetMainPageMyPosts(MainPageRequest pagedRequest, string userName);

        public Task<Post> GetPost(int id);

    }
}
