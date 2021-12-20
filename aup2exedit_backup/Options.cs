using CommandLine;
using CommandLine.Text;

namespace aup2exedit_backup
{
    internal class Options
    {
        [Value(0, Required = true, MetaName = "filename", HelpText = "aupファイルのパス")]
        public string Filename { get; set; } = string.Empty;

        [Option('o', "out", HelpText = "出力するexoファイルのパス")]
        public string OutputPath { get; set; } = string.Empty;

        [Usage]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("バックアップファイルを出力する", new Options() { Filename = @"C:\path\to\project.aup" });
                yield return new Example("出力ファイルのパスを指定する", new Options()
                {
                    Filename = @"C:\path\to\project.aup",
                    OutputPath = @"C:\path\to\backup.exedit_backup"
                });
            }
        }
    }
}
