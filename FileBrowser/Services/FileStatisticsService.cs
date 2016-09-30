using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FileBrowser.Services
{
    // Todo: refactor
    public class FileStatisticsService : IFileStatisticsService
    {
        private class FileBySizeCounter
        {
            private readonly long _minInclusive;

            private readonly long _maxInclusive;

            public readonly string Description;

            public int Number { get; private set; }

            public FileBySizeCounter(string description, long minInclusive, long maxInclusive)
            {
                Description = description;
                _minInclusive = minInclusive;
                _maxInclusive = maxInclusive;
            }

            public void Count(FileInfo file)
            {
                if (file.Length >= _minInclusive && file.Length <= _maxInclusive)
                {
                    Number++;
                }
            }
        }

        private readonly FileBySizeCounter[] _bySizeCounters = new []
        {
            new FileBySizeCounter("≤10MB", 0, 10 * 1024 * 1024),
            new FileBySizeCounter("10MB - 50MB", 10 * 1024 * 1024 + 1, 50 * 1024 * 1024),
            new FileBySizeCounter("≥100MB", 100 * 1024 * 1024, long.MaxValue)
        };

        private readonly IFileSystemService _fileSystemService;

        public FileStatisticsService(IFileSystemService fileSystemService)
        {
            _fileSystemService = fileSystemService;
        }

        public Dictionary<string, int> GetFileSizeStatistics(string directoryPath, CancellationToken ct)
        {
            foreach (var file in _fileSystemService.EnumerateFiles(directoryPath, SearchOption.AllDirectories))
            {
                ct.ThrowIfCancellationRequested();
                UpdateStatisticsWithFile(file);
            }

            return _bySizeCounters.ToDictionary(counter => counter.Description, counter => counter.Number);
        }

        private void UpdateStatisticsWithFile(FileInfo file)
        {
            foreach (var counter in _bySizeCounters) counter.Count(file);
        }
    }
}
