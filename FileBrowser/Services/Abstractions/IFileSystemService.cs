using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileBrowser.Services
{
    public interface IFileSystemService
    {
        IEnumerable<string> EnumerateDirectoryNames(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        IEnumerable<string> EnumerateFileNames(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        IEnumerable<FileInfo> EnumerateFiles(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly);

        IEnumerable<DriveInfo> EnumerateAvailableDrives();

        string GetFileName(string path);
    }
}