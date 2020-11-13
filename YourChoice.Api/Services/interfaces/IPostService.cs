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

        public Task<Post> CreatePhotoPost(CreatePostDto postDto);

        public Task<PostCardDto> GetPostGards(int number);

        public Task<Post> GetPost(int id);

    }
}
