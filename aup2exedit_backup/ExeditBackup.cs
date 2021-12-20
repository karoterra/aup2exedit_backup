using Karoterra.AupDotNet;
using Karoterra.AupDotNet.Extensions;

namespace aup2exedit_backup
{
    internal class ExeditBackup
    {
        public uint FormatVersion { get; set; }
        public uint ExeditVersion { get; set; }
        public int FrameNum { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int VideoRate { get; set; }
        public int VideoScale { get; set; }
        public int AudioRate { get; set; }

        public byte[] Data { get; set; } = Array.Empty<byte>();

        public ExeditBackup()
        {
        }

        public ExeditBackup(EditHandle editHandle, byte[] data)
        {
            Data = data;

            var span = new ReadOnlySpan<byte>(data);
            FormatVersion = span.ToUInt32();
            ExeditVersion = span[0x2C..].ToUInt32();
            FrameNum = editHandle.FrameNum;
            Width = editHandle.Width;
            Height = editHandle.Height;
            VideoRate = editHandle.VideoRate;
            VideoScale = editHandle.VideoScale;
            AudioRate = editHandle.AudioRate;
        }

        public bool Write(string path)
        {
            try
            {
                using FileStream fs = File.Create(path);
                using BinaryWriter writer = new(fs);

                writer.Write(FormatVersion);
                writer.Write(ExeditVersion);
                writer.Write(FrameNum);
                writer.Write(Width);
                writer.Write(Height);
                writer.Write(VideoRate);
                writer.Write(VideoScale);
                writer.Write(AudioRate);
                writer.Write(Data.Length);
                writer.Write(Data);
            }
            catch (UnauthorizedAccessException)
            {
                Console.Error.WriteLine($"{path} へのアクセス許可がありません。");
                return false;
            }
            catch (PathTooLongException)
            {
                Console.Error.WriteLine("出力パスが長すぎます。");
                return false;
            }
            catch (Exception e) when (
                e is ArgumentException or
                ArgumentNullException or
                DirectoryNotFoundException or
                NotSupportedException)
            {
                Console.Error.WriteLine("有効な出力パスを指定してください。");
                return false;
            }
            catch (IOException)
            {
                Console.Error.WriteLine("ファイル作成中にIOエラーが発生しました。");
                return false;
            }
            return true;
        }
    }
}
