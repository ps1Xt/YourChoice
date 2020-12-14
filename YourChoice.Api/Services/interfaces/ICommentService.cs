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
        public Task<Comment> CreateComment(CreateCommentDto commentDto, string userName);

        public Task<Comment> GetComment(int id);
    }
}
