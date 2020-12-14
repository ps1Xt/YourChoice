using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Dtos.Comment
{
    public class CommentDto
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int Id { get; set; }

        [Required]
        public string Text { get; set; }

        public string Date { get; set; }


    }
}
