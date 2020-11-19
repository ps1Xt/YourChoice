using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions.Post;
using YourChoice.Api.Repositories.interfaces;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Exceptions;

namespace YourChoice.Api.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IRepository repository;

        private readonly IMapper mapper;

        public PostService(IRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Post> CreatePost(CreatePostDto postDto)
        {
            List<PostPartDto> partsDto = postDto.PartsDto;

            if (postDto.Size != partsDto.Count)
            {
                throw new SizeException("The size does not correspond to the number of parts");
            }

            Post post = new Post();
            post = mapper.Map<Post>(postDto);
            //  post.User = await repository.GetById<User>(post.UserId);
            // post.PostParts = mapper.Map<List<PostPart>>(postDto.PartsDto);


            await repository.Add<Post>(post);
            await repository.SaveAll();
            return post;
        }


        public async Task<Post> GetPost(int id)
        {
            var post = await repository.GetById<Post>(id);
            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }
            return post;
        }

    }
}
