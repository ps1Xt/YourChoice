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
        public string Id { get; set; }

        public string Title { get; set; }

        public string UserName { get; set; }

        public string UserId { get; set; }

        public DateTime Date { get; set; }

        public List<CommentDto> Comments { get; set; }

        public List<PostPartDto> PartsDto { get; set; }
    }
}
