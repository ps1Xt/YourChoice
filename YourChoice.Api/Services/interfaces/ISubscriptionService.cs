using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Domain;

namespace YourChoice.Api.Services.interfaces
{
    public interface ISubscriptionService
    {
        public Task<bool> Subscribe(string whoUserName, string toWhomUserName);

        public Task<bool> UnSubscribe(string whoUserName, string toWhomUserName);

    }
}
