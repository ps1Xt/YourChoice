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
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.AvgRating, opt => opt
                .MapFrom(src => src.Ratings.Count == 0 ? 0 : src.Ratings.Select(x => x.Value).Average()))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("MM/dd/yyyy HH:mm")));

            CreateMap<PostPart, PostPartDto>();

            CreateMap<CreatePostDto, Post>()
                .ForMember(dest => dest.PostParts, opt => opt.MapFrom(src => src.PartsDto));

            CreateMap<PostPartDto, PostPart>();

            CreateMap<Post, PostCardDto>()
                .ForMember(dest => dest.AvgRating, opt => opt
                .MapFrom(src => src.Ratings.Count == 0 ? 0 : src.Ratings.Select(x => x.Value).Average()))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName));

            CreateMap<Post, PostGridRowDto>()
                .ForMember(dest => dest.Favorites, opt => opt.MapFrom(src => src.Favorites.Count()))
                .ForMember(dest => dest.AvgRating, opt => opt
                .MapFrom(src => src.Ratings.Count == 0 ? 0 : src.Ratings.Select(x => x.Value).Average()));

        }
    }

}
