namespace Client.Classes
{
    public class CompareAndDownloadFilesClass
    {
        public static void CompareAndDownloadFiles(string serverPath, string UserName, string Password, string DownloadFolderPath, string relativePath = "")
        {
            try
            {
                string ftpUrl = $"ftp://{UserName}:{Password}@{serverPath}";

                List<string> serverFiles = GetFileListClass.GetFileList(ftpUrl);

                string localFolderPath = DownloadFolderPath;
                if (!Directory.Exists(localFolderPath))
                {
                    Directory.CreateDirectory(localFolderPath);
                }
                string[] localFiles = Directory.GetFiles(localFolderPath, "*", SearchOption.AllDirectories);

                foreach (string localFile in localFiles)
                {
                    string relativeFilePath = localFile.Substring(localFolderPath.Length + 1);
                    string serverFilePath = $"{serverPath}/{relativeFilePath}";

                    if (serverFiles.Contains(serverFilePath))
                    {
                        long localFileSize = GetLocalFileSizeClass.GetLocalFileSize(localFile);
                        long serverFileSize = GetFileSizeClass.GetFileSize(serverFilePath, UserName, Password);

                        if (localFileSize != serverFileSize)
                        {
                            DownloadFileClass.DownloadFile(serverFilePath, localFile, UserName, Password);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error comparing and downloading files: {ex.Message}");
            }
        }
    }
}