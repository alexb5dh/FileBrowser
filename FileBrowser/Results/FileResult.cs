using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace FileBrowser.Results
{
    public class FileResult : IHttpActionResult
    {
        private readonly string _filePath;

        public FileResult(string filePath)
        {
            _filePath = filePath;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return new HttpResponseMessage
            {
                Content = new StreamContent(File.OpenRead(_filePath))
                {
                    Headers =
                    {
                        ContentType = new MediaTypeHeaderValue("application/octet-stream"),
                        ContentDisposition = new ContentDispositionHeaderValue("attachment")
                        {
                            FileName = Path.GetFileName(_filePath)
                        }
                    }
                }
            };
        }
    }
}