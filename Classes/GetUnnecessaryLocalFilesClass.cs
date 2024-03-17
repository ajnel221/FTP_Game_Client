namespace Client.Classes
{
    public class GetUnnecessaryLocalFilesClass
    {
        public static List<string> GetUnnecessaryLocalFiles(List<string> serverFiles, string localFolderPath, string serverPath)
        {
            List<string> unnecessaryFiles = new List<string>();
            List<string> serverFilePaths = GetFilePathsClass.GetFilePaths(serverPath);
            List<string> newKak = new List<string>();

            string[] localFiles = Directory.GetFiles(localFolderPath, "*", SearchOption.AllDirectories);

            foreach (string item in serverFilePaths)
            {
                string[] temp = item.Split($"{serverPath}/");
                newKak.Add(temp[1]);
            }

            foreach (string localFile in localFiles)
            {
                string localRelativePath = localFile.Substring(localFolderPath.Length + 1).Replace('\\', '/');

                if (!newKak.Contains(localRelativePath))
                {
                    unnecessaryFiles.Add(localRelativePath);
                }
            }

            return unnecessaryFiles;
        }
    }
}