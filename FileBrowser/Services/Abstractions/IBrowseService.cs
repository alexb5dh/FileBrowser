using System.Threading;
using FileBrowser.Models;

namespace FileBrowser.Services
{
    public interface IBrowseService
    {
        DirectoryContent GetDirectoryContent(string directoryPath, CancellationToken ct);
    }
}