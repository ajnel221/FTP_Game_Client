namespace Client.Classes
{
    public class GetLocalFileSizeClass
    {
        public static long GetLocalFileSize(string filePath)
        {
            return File.Exists(filePath) ? new FileInfo(filePath).Length : 0;
        }
    }
}