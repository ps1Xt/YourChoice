using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Message;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions;
using YourChoice.Api.Repositories.Interfaces;
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
        private readonly IMessageRepository messageRepository;

        public NotificationService(IRepository repository, IMapper mapper, UserManager<User> userManager, IMessageRepository messageRepository)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.userManager = userManager;
            this.messageRepository = messageRepository;
        }

        public async Task<bool> CommentNotify(int postId, string whoseComment)
        {

            var post = await repository.GetById<Post>(postId);

            var userName = post.User.UserName;

            if (whoseComment == userName)
                return false;

            string title = "New Comment";

            string text = $"{whoseComment} left a comment";

            return await NotifyUser(title, text, userName);
        }

        public async Task<bool> FavoritesNotify(int postId, string whoAdded)
        {
            

            var post = await repository.GetById<Post>(postId);

            var userName = post.User.UserName;

            if (whoAdded == userName)
                return false;

            string title = "Favorites";

            string text = $"{whoAdded} added your post to favorites";

            return await NotifyUser(title, text, userName);
        }

        public async Task<bool> RatingNotify(PostRatingDto postRatingDto, string whoRated)
        {
            var post = await repository.GetById<Post>(postRatingDto.Id);

            var userName = post.User.UserName;

            if (whoRated == userName)
                return false;

            string title = "Rating";

            string text = $"{whoRated} Rated your post by {postRatingDto.Value}";

            return await NotifyUser(title, text, userName);
        }

        private async Task<bool> NotifyUser(string title, string text, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            Message message = new Message();

            message.User = user;

            message.Title = title;

            message.Text = text;

            user.Messages.Add(message);

            await repository.SaveAll();

            return true;
        }

        private async Task<bool> NotifyUsers(string title, string text, List<User> users)
        {
            Message message = new Message();

            message.Title = title;

            message.Text = text;

            foreach (var user in users)
            {
                user.Messages.Add(message);
            }

            await repository.SaveAll();

            return true;
        }

        public async Task<bool> SubscribersNotify(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var users = user.Subscribers.Where(x => x.WhoId != user.Id).Where(x => x.Value == true).Select(x => x.Who).ToList();

            string title = "New Post";

            string text = $"{userName} publicated a new post";

            return await NotifyUsers(title, text, users);
        }

      

        public async Task<bool> SubscriptionNotify(string userName, string whoSubscribed)
        {
            if (userName == whoSubscribed)
                return false;

            string title = "New subscriber";

            string text = $"{whoSubscribed} subscribed to you";

            return await NotifyUser(title, text, userName);

        }

        public async Task<List<MessageDto>> GetMessages(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var messages = messageRepository.GetLastMessages(user.Id);

            var resultMessages = mapper.Map<List<MessageDto>>(messages);

            return resultMessages;
        }


        public async Task<bool> ReadMessages(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var messages = user.Messages.Where(x => x.IsRead == false);

            foreach (var message in messages)
            {
                message.IsRead = true;
            }

            await repository.SaveAll();

            return true;
        }

        public async Task<int> GetCountOfUnreadMessages(string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var unreadMessages = user.Messages.Count(x => x.IsRead == false);

            return unreadMessages;
        }
    }
}
