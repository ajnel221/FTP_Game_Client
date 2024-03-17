using System.Net;

namespace Client.Classes
{
    public class GetFileListClass
    {
        public static List<string> GetFileList(string ftpUrl)
        {
            List<string> fileList = new List<string>();

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(ftpUrl);
            request.Method = WebRequestMethods.Ftp.ListDirectory;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            using (Stream responseStream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(responseStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (line != "." && line != "..")
                    {
                        fileList.Add(line);
                    }
                }
            }

            return fileList;
        }
    }
}