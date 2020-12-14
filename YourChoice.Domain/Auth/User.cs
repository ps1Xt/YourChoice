using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace YourChoice.Domain.Auth
{
    public class User : IdentityUser<int>
    {
        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
        public virtual List<Subscription> Subscribers { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public DateTime RegistrationDate { get; set; }

        public User()
        {
            RegistrationDate = DateTime.Now;
        }
    }
}
