using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using YourChoice.Domain;

namespace YourChoice.Api.Dtos.Post
{
    public class CreatePostDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "Title is too big")]
        public string Title { get; set; }

        [Required]
        public int Size { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "Please append logo")]
        public string Logo { get; set; }

        public List<PostPartDto> PartsDto { get; set; }
    }
}
