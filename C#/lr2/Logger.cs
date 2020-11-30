using System.IO;
using System.Threading;

namespace Lr2WindowsService
{
    public class Logger
    {
        private FileSystemWatcher sourceFolderWatcher;
        private FileSystemWatcher targetFolderWatcher;
        private FileSystemWatcher extractFolderWatcher;

        private readonly string targetDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr2\archive";
        private readonly string sourceDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr2\source";
        private readonly string extractDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr2\extract";
        private bool enabled = true;

        public Logger()
        {
            sourceFolderWatcher = new FileSystemWatcher(sourceDirectoryPath);
            sourceFolderWatcher.Created += SourceFolderWatcher_Created;
            targetFolderWatcher = new FileSystemWatcher(targetDirectoryPath);
            targetFolderWatcher.Created += TargetFolderWatcher_Created;
            extractFolderWatcher = new FileSystemWatcher(extractDirectoryPath);

        }

        public void Start()
        {
            sourceFolderWatcher.EnableRaisingEvents = true;

            targetFolderWatcher.EnableRaisingEvents = true;

            while (enabled)
            {
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            sourceFolderWatcher.EnableRaisingEvents = false;

            targetFolderWatcher.EnableRaisingEvents = false;

            enabled = false;
        }

        private void SourceFolderWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Archivator.Archive(e.FullPath, targetDirectoryPath);
        }

        private void TargetFolderWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Thread.Sleep(1000);
            Archivator.Dearchive(e.FullPath, extractDirectoryPath);
        }
    }
}
