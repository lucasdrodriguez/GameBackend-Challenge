using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Darewise_Challenge.Services
{
    public class LocalFileManager : IFilesManager
    {
        private readonly IWebHostEnvironment env;
        private readonly IHttpContextAccessor httpContextAccessor;

        public LocalFileManager(IWebHostEnvironment env,
            IHttpContextAccessor httpContextAccessor)
        {
            this.env = env;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> SaveFile(byte[] content, string extension, string container,
            string contentType)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";
            string folder = Path.Combine(env.WebRootPath, container);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            string path = Path.Combine(folder, fileName);
            await File.WriteAllBytesAsync(path, content);

            var urlActual = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}";
            var urlParaBD = Path.Combine(urlActual, container, fileName).Replace("\\", "/");
            return urlParaBD;
        }
    }

}
