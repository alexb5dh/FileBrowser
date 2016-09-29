using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FileBrowser.Services
{
    public interface IFileStatisticsService
    {
        Dictionary<string, int> GetFileSizeStatistics(string directoryPath, CancellationToken ct);
    }
}
