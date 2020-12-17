using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Comment;
using YourChoice.Domain;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Services.interfaces
{
    public interface ICommentService
    {
        public Task<CommentDto> CreateComment(CreateCommentDto commentDto, string userName);

        public Task<CommentDto> GetComment(int id);
    }
}
