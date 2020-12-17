using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Dtos.Comment
{
    public class CommentDto
    {
        public string UserName { get; set; }

        public int UserId { get; set; }

        public int Id { get; set; }

        public string Text { get; set; }

        public string Date { get; set; }


    }
}
