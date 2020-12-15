using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Exceptions;
using YourChoice.Api.Repositories.interfaces;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Services.implementation
{
    public class NotificationService : INotificationService
    {
        private readonly IRepository repository;
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;

        public NotificationService(IRepository repository, IMapper mapper, UserManager<User> userManager)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
        }

        public async Task<Message> CommentNotify(int postId, string whoseComment)
        {

            var post = await repository.GetById<Post>(postId);

            var userName = post.User.UserName;

            string title = "New Comment";

            string text = $"{whoseComment} left a comment";

            return await Notify(title, text, userName);
        }

        public async Task<Message> FavoritesNotify(int postId, string whoLikedUserName)
        {
            var post = await repository.GetById<Post>(postId);

            var userName = post.User.UserName;

            string title = "Favorites";

            string text = $"{whoLikedUserName} added your post to favorites";

            return await Notify(title, text, userName);
        }

        private async Task<Message> Notify(string title, string text, string userName)
        {
            Message message = new Message();

            message.User = await userManager.FindByNameAsync(userName);

            message.Title = title;

            message.Text = text;

            await repository.Add(message);

            await repository.SaveAll();

            return message;
        }

        public async Task<Message> SubscribersNotify(string userName, string whosePost)
        {
            string title = "New Post";

            string text = $"{whosePost} publicated a new post";

            return await Notify(title, text, userName);
        }

      

        public async Task<Message> SubscriptionNotify(string userName, string whoSubscribed)
        {

            string title = "New subscriber";

            string text = $"{whoSubscribed} subscribed to you";

            return await Notify(title, text, userName);

        }

        public async Task<List<Message>> GetMessages(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);
            var messages = await repository.Find<Message>(x => x.UserId == user.Id && x.Date.AddDays(-1) < DateTime.Now);

            return messages.Reverse().ToList();
        }


        public async Task<bool> ReadMessages(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var messages = user.Messages.Where(x => !x.IsRead);

            foreach (var message in messages)
            {
                message.IsRead = true;
            }

            await repository.SaveAll();

            return true;
        }

    }
}
