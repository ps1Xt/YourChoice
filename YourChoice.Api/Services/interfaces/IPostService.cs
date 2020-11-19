using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Domain;
using YourChoice.Api.Dtos.Post;
namespace YourChoice.Api.Services.interfaces
{
    public interface IPostService
    {

        public Task<Post> CreatePost(CreatePostDto postDto);

//        public Task<List<Post>> GetPagedPosts(int page, string order);

        public Task<Post> GetPost(int id);

    }
}
