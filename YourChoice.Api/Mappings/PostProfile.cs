using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Controllers;
using YourChoice.Api.Dtos.Post;
using YourChoice.Domain;

namespace YourChoice.Api.Mappings
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, FullPostDto>();
            CreateMap<PostPart, PostPartDto>();
            CreateMap<CreatePostDto, Post>();
            CreateMap<PostPartDto, PostPart>();
        }
    }

}
