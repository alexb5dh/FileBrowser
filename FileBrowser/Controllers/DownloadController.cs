using System.Threading;
using System.Web.Http;
using FileBrowser.Results;

namespace FileBrowser.Controllers
{
    public class DownloadController : ApiController
    {
        public FileResult Get(string path, CancellationToken ct)
        {
            return new FileResult(path);
        }
    }
}