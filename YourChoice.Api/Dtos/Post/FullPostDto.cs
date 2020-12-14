using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Api.Dtos.Comment;

namespace YourChoice.Api.Dtos.Post
{
    public class FullPostDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string UserName { get; set; }

        public int UserId { get; set; }

        public string Description { get; set; }

        public bool isInFavorites { get; set; }

        public int Size { get; set; }

        public bool isSubscribed { get; set; }

        public double AvgRating { get; set; }

        public string Date { get; set; }

        public List<PostPartDto> PostParts { get; set; }

        public List<CommentDto> Comments { get; set; }
    }
}
