using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain.interfaces;
namespace YourChoice.Domain
{
    public class PhotoPostPart : PostPart, INaviagtionProperty<PhotoPost>
    {
        public string PhotoLink { get; set; }
        public virtual PhotoPost Post { get; set; }
        public int? PhotoPostId { get; set; }
    }
}
