using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Post;

namespace YourChoice.Api.Services.interfaces
{
    public interface IRatingService
    {
        public Task<double> RatePost(PostRatingDto postRatingDto, string userName);
    }
}
