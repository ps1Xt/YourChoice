using System;
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

        public Task<Post> CreatePost(IFormCollection form, string userName);

        public Task<PaginatedResult<PostGridRowDto>> GetPage(PagedRequest pagedRequest);

        public Task<PaginatedResult<PostCardDto>> GetMainPageHome(PagedRequest pagedRequest);

        public Task<PaginatedResult<PostCardDto>> GetMainPageSubscriptions(PagedRequest pagedRequest, string userName);

        public Task<PaginatedResult<PostCardDto>> GetMainPageFavorites(PagedRequest pagedRequest, string userName);

        public Task<PaginatedResult<PostCardDto>> GetMainPageMyPosts(PagedRequest pagedRequest, string userName);

        public Task<FullPostDto> GetPost(int id, string userName);

    }
}
