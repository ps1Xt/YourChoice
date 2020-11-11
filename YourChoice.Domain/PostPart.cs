using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YourChoice.Domain
{
    public class PostPart : BaseEntity
    {
        public string Title { get; set; }
    }
}
