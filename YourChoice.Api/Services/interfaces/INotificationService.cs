using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Message;
using YourChoice.Api.Dtos.Post;
using YourChoice.Domain;

namespace YourChoice.Api.Services.interfaces
{
    public interface INotificationService
    {
        public Task<bool> CommentNotify(int postId, string whoseComment);

        public Task<bool> SubscribersNotify(string userName);

        public Task<bool> FavoritesNotify(int postId, string whoAdded);

        public Task<bool> SubscriptionNotify(string userName, string whoSubscribed);

        public Task<int> GetCountOfUnreadMessages(string userName);

        public Task<bool> RatingNotify(PostRatingDto postRatingDto, string whoRated);

        public Task<List<MessageDto>> GetMessages(string userName);

        public Task<bool> ReadMessages(string userName);
    }
}
