using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Infrastructure.Models;

namespace YourChoice.Api.Repositories.Interfaces
{
    public interface IPostRepository : IRepository
    {
        public Task<PaginatedResult<PostCardDto>> GetMainPageMyPosts(PagedRequest pagedRequest, string userName);

        public Task<PaginatedResult<PostCardDto>> GetMainPageSubscriptions(PagedRequest pagedRequest, int userId);

        public Task<PaginatedResult<PostCardDto>> GetMainPageFavorites(PagedRequest pagedRequest, int userId);
    }
}
