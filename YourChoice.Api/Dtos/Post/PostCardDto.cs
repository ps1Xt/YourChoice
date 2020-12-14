using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Dtos.Post
{
    public class PostCardDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Date { get; set; } 

        public string Description { get; set; }

        public double AvgRating { get; set; }

        public string UserName { get; set; }

        public string Logo { get; set; }
    }
}
