using System.Text;
using Karoterra.AupDotNet;
using CommandLine;

namespace aup2exedit_backup;

class Program
{
    static int Main(string[] args)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        return Parser.Default.ParseArguments<Options>(args)
            .MapResult(opt => Run(opt), err => 1);
    }
    static AviUtlProject? ReadAup(string path)
    {
        try
        {
            return new AviUtlProject(path);
        }
        catch (FileNotFoundException)
        {
            Console.Error.WriteLine($"\"{path}\" が見つかりませんでした。");
            return null;
        }
        catch (UnauthorizedAccessException)
        {
            Console.Error.WriteLine($"\"{path}\" はディレクトリであるか、アクセス許可がありません。");
            return null;
        }
        catch (PathTooLongException)
        {
            Console.Error.WriteLine("パスが長すぎます。");
            return null;
        }
        catch (Exception e) when (
            e is ArgumentException or
            ArgumentNullException or
            DirectoryNotFoundException or
            NotSupportedException)
        {
            Console.Error.WriteLine("有効なパスを指定してください。");
            return null;
        }
        catch (FileFormatException e)
        {
            Console.Error.WriteLine($"\"{path}\" は AviUtl プロジェクトファイルでないか破損している可能性があります。");
            Console.Error.WriteLine(e.Message);
            return null;
        }
        catch (EndOfStreamException)
        {
            Console.Error.WriteLine($"\"{path}\" は AviUtl プロジェクトファイルでないか破損している可能性があります。");
            Console.Error.WriteLine("ファイルの読み込み中に終端に達しました。");
            return null;
        }
        catch (IOException)
        {
            Console.Error.WriteLine("IOエラーが発生しました。");
            return null;
        }
    }

    static int Run(Options opt)
    {
        if (opt.Filename == opt.OutputPath)
        {
            Console.Error.WriteLine("入力ファイルと出力ファイルのパスが同じです。");
            return 1;
        }

        var aup = ReadAup(opt.Filename);
        if (aup == null) return 1;

        var exedit = aup.FilterProjects.FirstOrDefault(f => f.Name == "拡張編集");
        if (exedit == null)
        {
            Console.Error.WriteLine("拡張編集のデータが見つかりません。");
            return 1;
        }

        ExeditBackup backup = new(aup.EditHandle, exedit.DumpData());

        if (string.IsNullOrEmpty(opt.OutputPath))
        {
            opt.OutputPath = Path.Combine(
                Path.GetDirectoryName(opt.Filename) ?? string.Empty,
                $"{Path.GetFileNameWithoutExtension(opt.Filename)}.exedit_backup");
        }
        if (!backup.Write(opt.OutputPath))
        {
            return 1;
        }

        return 0;
    }
}
