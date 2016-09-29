using System.Collections.Generic;

namespace FileBrowser.Models
{
    public class DirectoryContent
    {
        public IEnumerable<FileSystemLink> Folders { get; set; }

        public IEnumerable<FileSystemLink> Files { get; set; }
    }
}