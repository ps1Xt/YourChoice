using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Comment;
using YourChoice.Api.Repositories.interfaces;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Services.implementation
{
    public class CommentService : ICommentService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public CommentService(IRepository repository, IMapper mapper, UserManager<User> userManager )
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.repository = repository;
        }
        public async Task<Comment> CreateComment(CreateCommentDto commentDto, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var comment = new Comment();

            comment.PostId = commentDto.PostId;

            comment.Text = commentDto.Text;

            comment.User = user;

            await repository.Add<Comment>(comment);

            await repository.SaveAll();

            return comment;
        }

        public async Task<Comment> GetComment(int id)
        {
            var result = await repository.GetById<Comment>(id);

            return result;
        }
    }
}
