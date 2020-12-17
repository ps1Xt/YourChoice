using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Comment;
using YourChoice.Api.Repositories.Interfaces;
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
        public async Task<CommentDto> CreateComment(CreateCommentDto commentDto, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var comment = new Comment()
            {
                PostId = commentDto.PostId,
                Text = commentDto.Text,
                User = user
            };


            await repository.Add<Comment>(comment);

            await repository.SaveAll();

            var resultComment = mapper.Map<CommentDto>(comment);

            return resultComment;
        }

        public async Task<CommentDto> GetComment(int id)
        {
            var result = await repository.GetById<Comment>(id);

            var resultComment = mapper.Map<CommentDto>(result);

            return resultComment;
        }
    }
}
