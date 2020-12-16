using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using YourChoice.Api.Services.interfaces;

namespace YourChoice.Api.Services.implementation
{
    public class PhotoService : IPhotoService
    {
        public async Task<(string, string)> UploadPhoto(Stream stream, string name)
        {
            using (var httpClient = new HttpClient())
            {
                var apiClient = new ApiClient("dc7aa47e53edfa9");

                var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
                var imageUpload = await imageEndpoint.UploadImageAsync(stream, null, null, name);

                return (imageUpload.Link, imageUpload.Title);
            }

        }

    }
}
