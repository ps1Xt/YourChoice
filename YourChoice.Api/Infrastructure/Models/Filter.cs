﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Infrastructure.Models
{
    public class Filter
    {
        public string Path { get; set; }
        public string Value { get; set; }
        public string Operation { get; set; }
    }
}
