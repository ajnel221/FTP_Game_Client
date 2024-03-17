using System.Net;

namespace Client.Classes
{
    public class DownloadAllFilesClass
    {
        public static void DownloadAllFiles(string serverPath, string UserName, string Password, string DownloadFolderPath, string relativePath = "")
        {
            try
            {
                string ftpUrl = $"ftp://{UserName}:{Password}@{serverPath}";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tokens = line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                        string fileName = tokens[tokens.Length - 1];
                        bool isDirectory = tokens[0][0] == 'd';

                        string fullPath = $"{serverPath}/{fileName}";
                        string localFilePath = Path.Combine(DownloadFolderPath, relativePath, fileName);

                        if (isDirectory)
                        {
                            Directory.CreateDirectory(localFilePath);
                            DownloadAllFilesClass.DownloadAllFiles(fullPath, UserName, Password, DownloadFolderPath, Path.Combine(relativePath, fileName));
                        }
                        else
                        {
                            DownloadFileClass.DownloadFile(fullPath, localFilePath, UserName, Password);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error downloading files: {ex.Message}");
            }
        }
    }
}