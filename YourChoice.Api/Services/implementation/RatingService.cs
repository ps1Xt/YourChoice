using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Post;
using YourChoice.Api.Exceptions;
using YourChoice.Api.Repositories.Interfaces;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Services.implementation
{
    public class RatingService : IRatingService
    {
        private readonly IRepository repository;
        private readonly UserManager<User> userManager;

        public RatingService(IRepository repository, UserManager<User> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }
        public async Task<double> RatePost(PostRatingDto postRatingDto, string userName)
        {
            var value = postRatingDto.Value;

            var postId = postRatingDto.Id;

            if (value > 10 || value < 1)
            {
                throw new BadRequestException("invalid value");
            }

            var user = await userManager.FindByNameAsync(userName);

            var post = await repository.GetById<Post>(postId);

            var rating = post.Ratings.SingleOrDefault(x => x.UserId == user.Id);

            if (rating == null)
            {
                rating = new Rating();
                rating.PostId = postId;
                rating.User = user;

                rating.Value = value;

                post.Ratings.Add(rating);

            }
            else
            {
                rating.Value = value;
            }

            await repository.SaveAll();

            return post.Ratings.Select(x => x.Value).Average();
        }
    }
}
