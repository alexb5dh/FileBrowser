using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileBrowser.Services
{
    // Todo: performance optimization
    public class FileSystemService : IFileSystemService
    {
        public IEnumerable<string> EnumerateDirectoryNames(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            try
            {
                var topDirectories = path == ""
                    ? EnumerateAvailableDrives().Select(drive => drive.Name)
                    : Directory.EnumerateDirectories(path);

                var directories = searchOption == SearchOption.TopDirectoryOnly
                    ? Enumerable.Empty<string>()
                    : topDirectories
                        .SelectMany(d => EnumerateDirectoryNames(d, searchOption));

                return directories.Concat(topDirectories);
            }
            catch (PathTooLongException)
            {
                return Enumerable.Empty<string>();
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
        }

        public IEnumerable<string> EnumerateFileNames(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            try
            {
                var topDirectories = path == ""
                    ? EnumerateAvailableDrives().Select(drive => drive.Name)
                    : Directory.EnumerateDirectories(path);

                var topFiles = path == ""
                    ? Enumerable.Empty<string>()
                    : Directory.EnumerateFiles(path);

                var files = searchOption == SearchOption.TopDirectoryOnly
                    ? Enumerable.Empty<string>()
                    : topDirectories
                        .SelectMany(f => EnumerateFileNames(f, searchOption));

                return files.Concat(topFiles);
            }
            catch (PathTooLongException)
            {
                return Enumerable.Empty<string>();
            }
            catch (UnauthorizedAccessException)
            {
                return Enumerable.Empty<string>();
            }
        }

        public IEnumerable<FileInfo> EnumerateFiles(string path, SearchOption searchOption = SearchOption.TopDirectoryOnly)
        {
            return EnumerateFileNames(path, searchOption)
                .Select(filePath =>
                {
                    try
                    {
                        return new FileInfo(filePath);
                    }
                    catch (PathTooLongException)
                    {
                        return null;
                    }
                }).Where(info => info != null);
        }

        public IEnumerable<DriveInfo> EnumerateAvailableDrives()
        {
            return DriveInfo.GetDrives().Where(drive => drive.IsReady);
        }

        public string GetFileName(string path)
        {
            var name = Path.GetFileName(path);
            return name != "" ? name : path;
        }
    }
}
