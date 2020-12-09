namespace Lr2WindowsService
{
    public class Options
    {
        internal string TargetDirectoryPath { get; set; }

        internal string SourceDirectoryPath { get; set; }

        internal string ExtractDirectoryPath { get; set; }

        public Options()
        {
            TargetDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr3\archive";

            SourceDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr3\source";

            ExtractDirectoryPath = @"C:\PTUIR\3 semestr\ISP\Lr3\extract";
        }

    }

}