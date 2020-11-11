﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YourChoice.Domain
{
    public class Rating : BaseEntity
    {
        public virtual User User { get; set; }
        public int? UserId { get; set; }
        public virtual Post Post { get; set; }
        public int? PostId { get; set; }
        public int Value { get; set; }


    }
}
