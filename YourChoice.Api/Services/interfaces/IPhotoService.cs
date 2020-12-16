using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace YourChoice.Api.Services.interfaces
{
    public interface IPhotoService
    {
        public Task<(string, string)> UploadPhoto(Stream file, string name);

    }
}
