using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain.Auth;
namespace YourChoice.Domain
{
    public class Favorite
    {
        public virtual User User { get; set; }
        public int? UserId { get; set; }
        public virtual Post Post { get; set; }
        public int? PostId { get; set; }
        public bool Value { get; set; }

    }
}
