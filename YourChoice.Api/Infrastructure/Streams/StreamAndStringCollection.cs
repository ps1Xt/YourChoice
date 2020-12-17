using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace YourChoice.Api.Infrastructure.Streams
{
    public class StreamAndStringCollection : Collection<(Stream, string)>, IDisposable
    {
        public void Dispose()
        {
            foreach (var item in Items)
            {
                item.Item1.Close();
            }
        }
    }
}
