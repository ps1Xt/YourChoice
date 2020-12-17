using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Database;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Infrastructure.Extensions;
using YourChoice.Api.Infrastructure.Models;
using YourChoice.Api.Repositories.Interfaces;
using YourChoice.Domain;

namespace YourChoice.Api.Repositories.Implementation
{
    public class PostRepository : Repository, IPostRepository
    {
        public PostRepository(DataBaseContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public Task<PaginatedResult<PostCardDto>> GetMainPageMyPosts(PagedRequest pagedRequest, string userName)
        {
            var posts = context.Set<Post>().Where(x => x.User.UserName == userName);

            var pagedPosts = posts.CreatePaginatedResultAsync<Post, PostCardDto>(pagedRequest, mapper);

            return pagedPosts;
        }
        public Task<PaginatedResult<PostCardDto>> GetMainPageSubscriptions(PagedRequest pagedRequest, int userId)
        {
            var posts = context.Set<Post>().Where(x => x.User.Subscribers.Where(x => x.Value == true).Select(x => x.WhoId).Any(y => y == userId));

            var pagedPosts = posts.CreatePaginatedResultAsync<Post, PostCardDto>(pagedRequest, mapper);

            return pagedPosts;
        }
        public Task<PaginatedResult<PostCardDto>> GetMainPageFavorites(PagedRequest pagedRequest, int userId)
        {
            var posts = context.Set<Post>().Where(x => x.Favorites.Where(x => x.Value == true).Select(x => x.UserId).Contains(userId));

            var pagedPosts = posts.CreatePaginatedResultAsync<Post, PostCardDto>(pagedRequest, mapper);

            return pagedPosts;
        }

    }
}
