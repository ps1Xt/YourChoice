using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using YourChoice.Domain.interfaces;
namespace YourChoice.Domain
{
    public class VideoPostPart  : PostPart, INaviagtionProperty<VideoPost>
    {
        public string VideoLink { get; set; }
        public virtual VideoPost Post { get; set; }
        public int? VideoPostId { get; set; }


    }
    
}
