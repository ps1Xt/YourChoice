using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Dtos.Post
{
    public class PostGridRowDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Favorites { get; set; }

        public string Date { get; set; }

        public double AvgRating { get; set; }

        public int Size { get; set; }
    }
}
