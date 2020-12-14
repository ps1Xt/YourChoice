using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain.Auth;
namespace YourChoice.Domain
{
    public class Post : BaseEntity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual List<Favorite> Favorites { get; set; }
        public string Title { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public virtual List<PostPart> PostParts { get; set; }
        public virtual List<Rating> Ratings { get; set; }
        public int Size { get; set; }
        public string Logo { get; set; }

        public Post()
        {
            Date = DateTime.Now;
        }

    }
}
