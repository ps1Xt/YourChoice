using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace YourChoice.Domain
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual List<Post> Posts { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public virtual List<Message> Messages { get; set; }
        public virtual List<Rating> Rating { get; set; }
        public virtual List<Subscription> Subscribers { get; set; }
        public virtual List<Subscription> Subscriptions { get; set; }
        public DateTime? RegistrationDate { get; set; }

    }
}
