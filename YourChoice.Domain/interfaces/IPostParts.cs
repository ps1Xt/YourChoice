using System;
using System.Collections.Generic;
using System.Text;

namespace YourChoice.Domain.interfaces
{
    public interface IPostParts<T>
    {
        public List<T> PostParts { get; set; }
    }
}
