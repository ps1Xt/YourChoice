using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Message;
using YourChoice.Api.Repositories.Interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Hubs
{
    public class NotifyHub : Hub
    {
        private readonly UserManager<User> userManager;
        private readonly IRepository repository;

        public NotifyHub(UserManager<User> userManager, IRepository repository)
        {
            this.userManager = userManager;
            this.repository = repository;
        }

        public async Task FavoritesNotify(int postId)
        {
            var post = await repository.GetById<Post>(postId);

            if (post == null)
                return;
            if (post.User.UserName == Context.User.Identity.Name)
                return;

            var userName = post.User.UserName;

            await Clients.User(userName).SendAsync("NewMessage");

        }
        public async Task RatingNotify(int postId)
        {

            var post = await repository.GetById<Post>(postId);

            if (post == null)
                return;
            if (post.User.UserName == Context.User.Identity.Name)
                return;

            var userName = post.User.UserName;

            await Clients.User(userName).SendAsync("NewMessage");
        }
        public async Task SubscriptionNotify(int postId)
        {
            var post = await repository.GetById<Post>(postId);

            if (post == null)
                return;
            if (post.User.UserName == Context.User.Identity.Name)
                return;

            var userName = post.User.UserName;

            await Clients.User(userName).SendAsync("NewMessage");

        }
        public async Task SubscribersNotify()
        {
            var user = await userManager.FindByNameAsync(Context.User.Identity.Name);

            IReadOnlyList<string> userNames = user.Subscribers.Where(x => x.Who.UserName != user.UserName)
                .Where(x => x.Value == true).Select(x => x.Who.UserName).ToList();

            await Clients.Users(userNames).SendAsync("NewMessage");

        }
        public async Task CommentNotify(int postId)
        {
            var post = await repository.GetById<Post>(postId);

            if (post == null)
                return;

            if (post.User.UserName == Context.User.Identity.Name)
                return;

            var userName = post.User.UserName;

            await Clients.User(userName).SendAsync("NewMessage");

        }
    }
}
