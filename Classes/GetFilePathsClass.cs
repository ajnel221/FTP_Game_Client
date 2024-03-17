using System.Net;

namespace Client.Classes
{
    public class GetFilePathsClass
    {
        public static List<string> GetFilePaths(string serverPath)
        {
            List<string> filePaths = new List<string>();

            try
            {
                string ftpUrl = $"{serverPath}";

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
                request.UsePassive = true;
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

                        if (!isDirectory)
                        {
                            filePaths.Add(fullPath);
                        }
                        else
                        {
                            List<string> subDirectoryFiles = GetFilePaths(fullPath);
                            filePaths.AddRange(subDirectoryFiles);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error getting file paths: {ex.Message}");
            }

            return filePaths;
        }
    }
}