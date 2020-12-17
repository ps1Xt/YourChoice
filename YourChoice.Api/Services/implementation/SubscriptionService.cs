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
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository repository;
        private readonly UserManager<User> userManager;

        public SubscriptionService(IRepository repository, UserManager<User> userManager)
        {
            this.repository = repository;
            this.userManager = userManager;
        }

        public async Task<bool> Subscribe(string whoUserName, string toWhomUserName)
        {
            var who = await userManager.FindByNameAsync(whoUserName);

            var toWhom = await userManager.FindByNameAsync(toWhomUserName);

            var subscription = who.Subscriptions.SingleOrDefault(x => x.ToWhomId == toWhom.Id && x.WhoId == x.WhoId);

            if(subscription == null)
            {
                subscription = new Subscription();

                subscription.Who = who;

                subscription.ToWhom = toWhom;

                subscription.Value = true;

                who.Subscriptions.Add(subscription);

                await repository.SaveAll();

                return true;

            }

            subscription.Value = true;

            await repository.SaveAll();

            return false;

        }

        public async Task<bool> UnSubscribe(string whoUserName, string toWhomUserName)
        {
            var who = await userManager.FindByNameAsync(whoUserName);

            var toWhom = await userManager.FindByNameAsync(toWhomUserName);

            var subscription = who.Subscriptions.SingleOrDefault(x => x.ToWhomId == toWhom.Id && x.WhoId == x.WhoId);

            if (subscription != null)
            {
                subscription.Value = false;

                await repository.SaveAll();

                return true;

            }

            return false;
        }
    }
}
