using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Comment;
using YourChoice.Domain;
using AutoMapper;
namespace YourChoice.Api.Mappings
{
    public class CommentProfile : Profile
    {
        public CommentProfile()
        {
            CreateMap<Comment, CommentDto>()
                   .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.UserName))
                   .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString("MM/dd/yyyy HH:mm")));
        }
        
    }
}
