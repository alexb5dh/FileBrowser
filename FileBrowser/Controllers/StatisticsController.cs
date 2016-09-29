using System.Collections.Generic;
using System.Threading;
using System.Web.Http;
using FileBrowser.Services;

namespace FileBrowser.Controllers
{
    public class StatisticsController: ApiController
    {
        private readonly IFileStatisticsService _fileStatisticsService;
        public StatisticsController(IFileStatisticsService fileStatisticsService)
        {
            _fileStatisticsService = fileStatisticsService;
        }

        public Dictionary<string, int> Get(string path, CancellationToken ct)
        {
            return _fileStatisticsService.GetFileSizeStatistics(path ?? "", ct);
        }
    }
}
