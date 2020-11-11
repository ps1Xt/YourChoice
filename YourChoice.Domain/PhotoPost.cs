using System;
using System.Collections.Generic;
using System.Text;
using YourChoice.Domain.interfaces;

namespace YourChoice.Domain
{
    public class PhotoPost : Post, IPostParts<PhotoPostPart>
    {
        public virtual List<PhotoPostPart> PostParts { get; set; }
    }
}
