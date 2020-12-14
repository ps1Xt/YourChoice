using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Domain;

namespace YourChoice.Api.Services.interfaces
{
    public interface IFavoriteService
    {
        public Task<bool> AddToFavorites(int postId, string userName);


        public Task<bool> RemoveFromFavorites(int postId, string userName);
    }
}
