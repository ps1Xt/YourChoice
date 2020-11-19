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
            CreateMap<Post, FullPostDto>()
                .ForMember(dest => dest.PartsDto, opt => opt.MapFrom(src => src.PostParts));
            CreateMap<PostPart, PostPartDto>();
            CreateMap<CreatePostDto, Post>()
                .ForMember(dest => dest.PostParts, opt => opt.MapFrom(src => src.PartsDto));
            CreateMap<PostPartDto, PostPart>();
            CreateMap<Post, PostCardDto>()
                .ForMember(dest => dest.Likes, opt => opt.MapFrom(src => src.Rating.Count()));

        }
    }

}
