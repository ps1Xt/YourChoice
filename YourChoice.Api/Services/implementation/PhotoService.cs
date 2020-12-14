using Imgur.API.Authentication;
using Imgur.API.Endpoints;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using YourChoice.Api.Services.interfaces;

namespace YourChoice.Api.Services.implementation
{
    public class PhotoService : IPhotoService
    {
        public async Task<(string, string)> UploadPhoto(Stream stream, string name)
        {
            var apiClient = new ApiClient("3b5daf3c0707fed");
            var httpClient = new HttpClient();

            var imageEndpoint = new ImageEndpoint(apiClient, httpClient);
            var imageUpload = await imageEndpoint.UploadImageAsync(stream, null, null, name);

            return (imageUpload.Link, imageUpload.Title);
        }


    }
}
