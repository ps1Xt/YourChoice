using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Domain;

namespace YourChoice.Api.Services.interfaces
{
    public interface INotificationService
    {
        public Task<Message> CommentNotify(int postId, string whoseComment);

        public Task<Message> SubscribersNotify(string userName, string whosePost);

        public Task<Message> FavoritesNotify(int postId, string whoLikedUserName);

        public Task<Message> SubscriptionNotify(string userName, string whoSubscribed);

        public Task<List<Message>> GetMessages(string userName);

        public Task<bool> ReadMessages(string userName);
    }
}
