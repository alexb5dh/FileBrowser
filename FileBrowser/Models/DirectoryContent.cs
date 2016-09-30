using System.Collections.Generic;

namespace FileBrowser.Models
{
    public class DirectoryContent
    {
        public IEnumerable<FolderLink> Folders { get; set; }

        public IEnumerable<FileSystemLink> Files { get; set; }
    }
}