using System.Net;

namespace Client.Classes
{
    public class DownloadFileClass
    {
        public static void DownloadFile(string filePath, string localFilePath, string UserName, string Password)
        {
            using (WebClient ftpClient = new WebClient())
            {
                ftpClient.Credentials = new NetworkCredential(UserName, Password);
                string directoryPath = Path.GetDirectoryName(localFilePath);
                Directory.CreateDirectory(directoryPath);
                ftpClient.DownloadFile($"ftp://{UserName}:{Password}@{filePath}", localFilePath);
            }
        }
    }
}