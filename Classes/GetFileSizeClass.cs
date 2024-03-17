using System.Net;

namespace Client.Classes
{
    public class GetFileSizeClass
    {
        public static long GetFileSize(string filePath, string UserName, string Password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(filePath);
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
            {
                return response.ContentLength;
            }
        }
    }
}