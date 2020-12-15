using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions.Post;
using YourChoice.Api.Infrastructure.Models;
using YourChoice.Api.Repositories.interfaces;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;
using YourChoice.Exceptions;

namespace YourChoice.Api.Services.Implementation
{
    public class PostService : IPostService
    {
        private readonly IRepository repository;

        private readonly IMapper mapper;
        private readonly IPhotoService photoService;

        public PostService(IRepository repository, IMapper mapper, IPhotoService photoService)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.photoService = photoService;
        }

        public async Task<Post> CreatePost(IFormCollection form, User user)
        {
            Post post = new Post();

            post.Description = form["description"];

            post.UserId = user.Id;

            List<PostPart> postParts = new List<PostPart>();
            List<Task<(string, string)>> tasks = new List<Task<(string, string)>>();
            var files = form.Files;

            post.Size = files.Count - 1;

            var streams = new List<(Stream, string)>();
            foreach (var file in files)
            {
                streams.Add((file.OpenReadStream(), file.FileName));
            }

            foreach (var (stream, name) in streams)
            {
                tasks.Add(Task.Run<(string, string)>(() => photoService.UploadPhoto(stream, name)));
            }
            await Task.WhenAll(tasks);

            var title = tasks[0].Result.Item2;

            var logo = tasks[0].Result.Item1;

            post.Title = title;

            post.Logo = logo;

            var parts = tasks.Skip(1).Select(x=>x.Result).Select(x => new PostPart { Title = x.Item2, Link = x.Item1 }).ToList();

            post.PostParts = parts;

            await repository.Add<Post>(post);

            await repository.SaveAll();

            return post;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageFavorites(MainPageRequest pagedRequest, string userName)
        {
            var user = (await repository.Find<User>(x => x.UserName == userName)).SingleOrDefault();

            var mainPage = await repository.GetPagedDataWithAdditionalPredicate<Post, PostCardDto>
               (pagedRequest, x => x.Favorites.Where(x => x.Value == true).Select(x => x.UserId).Contains(user.Id));

            return mainPage;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageHome(MainPageRequest pagedRequest)
        {

            var mainPage = await repository.GetPagedData<Post, PostCardDto>(pagedRequest);

            return mainPage;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageMyPosts(MainPageRequest pagedRequest, string userName)
        {
            var user = (await repository.Find<User>(x => x.UserName == userName)).SingleOrDefault();

            var mainPage = await repository.GetPagedDataWithAdditionalPredicate<Post, PostCardDto>
               (pagedRequest, x => x.UserId == user.Id);

            return mainPage;
        }

        public async Task<PaginatedResult<PostCardDto>> GetMainPageSubscriptions(MainPageRequest pagedRequest, string userName)
        {
            var user = (await repository.Find<User>(x => x.UserName == userName)).SingleOrDefault();

            var mainPage = await repository.GetPagedDataWithAdditionalPredicate<Post, PostCardDto>
                (pagedRequest, x => x.User.Subscribers.Where(x => x.Value == true).Select(x => x.WhoId).Any(y => y == user.Id));

            return mainPage;
        }

        
        public async Task<PaginatedResult<PostGridRowDto>> GetPage(PagedRequest pagedRequest)
        {
            var pagedPosts = await repository.GetPagedData<Post, PostGridRowDto>(pagedRequest);

            return pagedPosts;
        }

        public async Task<Post> GetPost(int id)
        {
            var post = await repository.GetById<Post>(id);

            if (post == null)
            {
                throw new NotFoundException("Post not found");
            }

            return post;
        }

    }
}
