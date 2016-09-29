using System.IO;
using System.Linq;
using System.Threading;
using FileBrowser.Models;

namespace FileBrowser.Services
{
    public class BrowseService : IBrowseService
    {
        private readonly IFileSystemService _fileSystemService;

        public BrowseService(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public DirectoryContent GetDirectoryContent(string directoryPath, CancellationToken ct)
        {
            var parentLink = directoryPath.Length > 0
                ? new[] { new FolderLink { Name = "..", Path = Path.GetDirectoryName(directoryPath) } }
                : Enumerable.Empty<FolderLink>();

            var folders = parentLink
                .Concat(_fileSystemService.EnumerateDirectoryNames(directoryPath)
                                          .Select(path => new FolderLink { Name = _fileSystemService.GetFileName(path), Path = path })
                                          .OrderBy(link => link.Name));

            var files = _fileSystemService.EnumerateFileNames(directoryPath)
                                          .Select(path => new FileLink { Name = _fileSystemService.GetFileName(path), Path = path })
                                          .OrderBy(link => link.Name);

            return new DirectoryContent
            {
                Folders = folders.ToArray(),
                Files = files.ToArray()
            };
        }
    }
}
