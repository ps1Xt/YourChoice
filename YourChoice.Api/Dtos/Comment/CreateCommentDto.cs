using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Dtos.Comment
{
    public class CreateCommentDto
    {
        public int PostId { get; set; }

        public string Text { get; set; }
    }
}
