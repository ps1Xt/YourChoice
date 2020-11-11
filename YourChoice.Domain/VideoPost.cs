using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain.interfaces;
namespace YourChoice.Domain
{
    public class VideoPost : Post, IPostParts<VideoPostPart>
    {
        public virtual List<VideoPostPart> PostParts { get; set; }
    }
}
