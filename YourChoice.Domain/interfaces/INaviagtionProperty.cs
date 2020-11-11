using System;
using System.Collections.Generic;
using System.Text;

namespace YourChoice.Domain.interfaces
{
    public interface INaviagtionProperty<T>
    {
        public T Post { get; set; }
    }
}
