using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions.Post;
using YourChoice.Api.Infrastructure.Models;
using YourChoice.Api.Infrastructure.Streams;
using YourChoice.Api.Repositories.Interfaces;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;
using YourChoice.Exceptions;

namespace YourChoice.Api.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IPostRepository repository;

        private readonly IMapper mapper;
        private readonly IPhotoService photoService;
        private readonly UserManager<User> userManager;

        public PostService(IPostRepository repository, IMapper mapper, IPhotoService photoService, UserManager<User> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.photoService = photoService;
            this.userManager = userManager;
        }

        public async Task<Post> CreatePost(IFormCollection form, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            Post post = new Post();

            post.Description = form["description"];

            post.UserId = user.Id;

            List<PostPart> postParts = new List<PostPart>();

            List<Task<(string, string)>> tasks = new List<Task<(string, string)>>();
            var files = form.Files;

            post.Size = files.Count - 1;

            using (var streams = new StreamAndStringCollection())
            {
                foreach (var file in files)
                {
                    streams.Add((file.OpenReadStream(), file.FileName));
                }

                foreach (var (stream, name) in streams)
                {
                    tasks.Add(Task.Run<(string, string)>(() => photoService.UploadPhoto(stream, name)));
                }
                await Task.WhenAll(tasks);
            }

            var title = tasks[0].Result.Item2;

            var logo = tasks[0].Result.Item1;

            post.Title = title;

            post.Logo = logo;

            var parts = tasks.Skip(1).Select(x => x.Result).Select(x => new PostPart { Title = x.Item2, Link = x.Item1 }).ToList();

            post.PostParts = parts;

            await repository.Add<Post>(post);

            await repository.SaveAll();

            return post;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageFavorites(PagedRequest pagedRequest, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var mainPage = await repository.GetMainPageFavorites(pagedRequest, user.Id);

            return mainPage;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageHome(PagedRequest pagedRequest)
        {

            var mainPage = await repository.GetPagedData<Post, PostCardDto>(pagedRequest);

            return mainPage;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageMyPosts(PagedRequest pagedRequest, string userName)
        {

            var mainPage = await repository.GetMainPageMyPosts(pagedRequest, userName);

            return mainPage;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageSubscriptions(PagedRequest pagedRequest, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var mainPage = await repository.GetMainPageSubscriptions(pagedRequest, user.Id);

            return mainPage;
        }


        public async Task<PaginatedResult<PostGridRowDto>> GetPage(PagedRequest pagedRequest)
        {
            var pagedPosts = await repository.GetPagedData<Post, PostGridRowDto>(pagedRequest);

            return pagedPosts;
        }

        public async Task<FullPostDto> GetPost(int id, string userName)
        {
            var post = await repository.GetById<Post>(id);

            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }

            var resultPost = mapper.Map<FullPostDto>(post);

            resultPost.isInFavorites = post.Favorites.SingleOrDefault(x => x.User.UserName == userName)?.Value ?? false;

            resultPost.isSubscribed = post.User.Subscribers.SingleOrDefault(x => x.Who.UserName == userName)?.Value ?? false;

            return resultPost;
        }

    }

}
