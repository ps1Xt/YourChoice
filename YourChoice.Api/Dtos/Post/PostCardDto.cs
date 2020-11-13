using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Dtos.Post
{
    public class PostCardDto
    {
        public string Title { get; set; }

        public DateTime Date { get; set; } 

        public string Description { get; set; }

        public string Likes { get; set; }

        public string Login { get; set; }

        public string Logo { get; set; }
    }
}
