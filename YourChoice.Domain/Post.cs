using System;
using System.Collections.Generic;
using System.Text;

namespace YourChoice.Domain
{
    public class Post : BaseEntity
    {
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public virtual List<Rating> Rating { get; set; }
        public string Title { get; set; }
        public virtual List<Comment> Comments { get; set; }
        public DateTime Date { get; set; }
        public bool CanBePublished { get; set; }

    }
}
