using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YourChoice.Api.Dtos.Comment;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;
        private readonly INotificationService notificationService;

        public CommentController(ICommentService commentService, IMapper mapper,
            INotificationService notificationService)
        {
            this.commentService = commentService;
            this.mapper = mapper;
            this.notificationService = notificationService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment(int id)
        {
            var comment = await commentService.GetComment(id);

            var result = mapper.Map<CommentDto>(comment);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(CreateCommentDto commentDto)
        {
            var userName = User.Identity.Name;

            var comment = await commentService.CreateComment(commentDto, userName);

            var postId = commentDto.PostId;

            var result = mapper.Map<CommentDto>(comment);

            await notificationService.CommentNotify(postId, userName);

            return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, result);
        }
    }
}
