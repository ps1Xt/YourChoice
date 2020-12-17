using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Repositories.Interfaces;
using YourChoice.Api.Services.interfaces;
using YourChoice.Domain;
using YourChoice.Domain.Auth;

namespace YourChoice.Api.Services.implementation
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IRepository repository;
        private readonly UserManager<User> userManager;

        public FavoriteService(IRepository repository, UserManager<User> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }


        public async Task<bool> AddToFavorites(int postId, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var favorite = user.Favorites.SingleOrDefault(x => x.PostId == postId);

            if (favorite == null)
            {
                favorite = new Favorite();

                favorite.UserId = user.Id;

                favorite.PostId = postId;

                favorite.Value = true;

                user.Favorites.Add(favorite);

                await repository.SaveAll();

                return true;
            }

            favorite.Value = true;

            await repository.SaveAll();

            return false;

        }

        public async Task<bool> RemoveFromFavorites(int postId, string userName)
        {
            var user = await userManager.FindByNameAsync(userName);

            var favorite = user.Favorites.SingleOrDefault(x => x.PostId == postId);

            if (favorite != null)
            {
                favorite.Value = false;

                await repository.SaveAll();

                return true;

            }

            return false;
        }

    }
}
