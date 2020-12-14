using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YourChoice.Domain
{
    public class PostPart : BaseEntity
    {
        public string Title { get; set; }

        public string Link { get; set; }

        public virtual Post Post { get; set; }

        public int PostId { get; set; }
    }
}
