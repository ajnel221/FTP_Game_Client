namespace Client.Classes
{
    public class DeleteUnnecessaryLocalFilesClass
    {
        public static void DeleteUnnecessaryLocalFiles(List<string> unnecessaryFiles, string DownloadFolderPath)
        {
            try
            {
                foreach (string fileToDelete in unnecessaryFiles)
                {
                    if (File.Exists($"{DownloadFolderPath}/{fileToDelete}"))
                    {
                        File.Delete($"{DownloadFolderPath}/{fileToDelete}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting unnecessary files: {ex.Message}");
            }
        }
    }
}