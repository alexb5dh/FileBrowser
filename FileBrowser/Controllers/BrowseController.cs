using System.IO;
using System.Net;
using System.Threading;
using System.Web.Http;
using FileBrowser.Models;
using FileBrowser.Services;

namespace FileBrowser.Controllers
{
    public class BrowseController : ApiController
    {
        private readonly IBrowseService _browseService;

        public BrowseController(IBrowseService browseService)
        {
            _browseService = browseService;
        }

        public DirectoryContent Get(string path, CancellationToken ct)
        {
            try
            {
                return _browseService.GetDirectoryContent(path ?? "", ct);
            }
            catch (DirectoryNotFoundException)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
        }
    }
}
