using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;

namespace YourChoice.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostService service;
        private readonly IMapper mapper;

        public PostController(IPostService service, IMapper mapper)
        {
            this.service = service;
            this.mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var post = await service.GetPost(id);
            var result = mapper.Map<FullPostDto>(post);
            result = mapper.Map<FullPostDto>(post);
            result.PartsDto = mapper.Map<List<PostPartDto>>(post.PostParts);
            result.UserName = post.User.Login;
            return Ok(result);
        }


        [HttpPost]
        [ApiExceptionFilter]
        public async Task<IActionResult> CreatePhotoPost(CreatePostDto postDto)
        {
            var post = await service.CreatePhotoPost(postDto);

            FullPostDto result = new FullPostDto();
            
            result = mapper.Map<FullPostDto>(post);
            result.PartsDto = mapper.Map<List<PostPartDto>>(post.PostParts);
            result.UserName = post.User.Login;
            return CreatedAtAction(nameof(GetPost), new {id = post.Id }, result);
        }
        
    }
}
