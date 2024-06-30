
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace Noble.Api.Models
{
    public class FileResult : IHttpActionResult
    {
        public FileResult(string filePath)
        {
            if (filePath == null)
                throw new ArgumentNullException(nameof(filePath));

            FilePath = filePath;
        }

        public string FilePath { get; }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            string contentType;
            new FileExtensionContentTypeProvider().TryGetContentType(FilePath, out contentType);
            response.Content = new StreamContent(File.OpenRead(FilePath));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(contentType ?? "application/octet-stream");
            return Task.FromResult(response);
        }
    }
}