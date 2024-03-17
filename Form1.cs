using System.ComponentModel;
using System.Diagnostics;
using Client.Classes;

namespace Client
{
    public partial class Form1 : Form
    {
        private string ServerIP;
        private string UserName;
        private string Password;
        private string Port;

        private BackgroundWorker bgWorker;
        private string SettingsDirectoryPath = $@"{Environment.CurrentDirectory}\Settings";
        private string SettingsPath = $@"{Environment.CurrentDirectory}\Settings\settings.txt";
        private string DownloadFolderPath = $@"{Environment.CurrentDirectory}\Game";
        private long totalBytesToDownload = 0;
        private long totalBytesDownloaded = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            LoadSettings();

            playBtn.Enabled = false;
            statusLabel.Text = "Waiting";
        }

        private void LoadSettings()
        {
            try
            {
                if (!Directory.Exists(SettingsDirectoryPath))
                {
                    Directory.CreateDirectory(SettingsDirectoryPath);
                }

                if (!File.Exists(SettingsPath))
                {
                    using (File.Create(SettingsPath)) { }
                    MessageBox.Show("A new settings file has been created. Please edit it and retry.");
                    return;
                }

                using (StreamReader reader = new StreamReader(SettingsPath))
                {
                    string[] lines = reader.ReadToEnd().Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                    if (lines.Length >= 4)
                    {
                        ServerIP = lines[0];
                        UserName = lines[1];
                        Password = lines[2];
                        Port = lines[3];

                        MessageBox.Show($"Network Connection: ftp://{UserName}:{Password}@{ServerIP}:{Port}");
                    }
                    else
                    {
                        MessageBox.Show("The settings file does not contain enough lines or contains too many lines.");
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show($"Error accessing the settings file: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void InitializeBackgroundWorker()
        {
            bgWorker = new BackgroundWorker();
            bgWorker.WorkerReportsProgress = true;
            bgWorker.DoWork += BgWorker_DoWork;
            bgWorker.ProgressChanged += BgWorker_ProgressChanged;
            bgWorker.RunWorkerCompleted += BgWorker_RunWorkerCompleted;
        }

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string serverFolderUrl = $"ftp://{UserName}:{Password}@{ServerIP}:{Port}/Game";

                List<string> serverFiles = GetFilePathsClass.GetFilePaths(serverFolderUrl);

                if (Directory.GetFiles(DownloadFolderPath).Length == 0 && Directory.GetDirectories(DownloadFolderPath).Length == 0)
                {
                    totalBytesToDownload = 0;
                    foreach (string filePath in serverFiles)
                    {
                        totalBytesToDownload += GetFileSizeClass.GetFileSize(filePath, UserName, Password);
                    }
                    DownloadAllFilesClass.DownloadAllFiles($"{ServerIP}:{Port}/Game", UserName, Password, DownloadFolderPath);
                }
                else
                {
                    totalBytesToDownload = 0;
                    bgWorker.ReportProgress(0, "Checking Files");
                    foreach (string filePath in serverFiles)
                    {
                        totalBytesToDownload += GetFileSizeClass.GetFileSize(filePath, UserName, Password);
                    }

                    totalBytesDownloaded = 0;
                    foreach (string localFile in Directory.GetFiles(DownloadFolderPath, "*", SearchOption.AllDirectories))
                    {
                        totalBytesDownloaded += GetLocalFileSizeClass.GetLocalFileSize(localFile);
                    }

                    long totalBytesRemaining = totalBytesToDownload - totalBytesDownloaded;

                    if (totalBytesRemaining > 0)
                    {
                        int progressPercentage = (int)(((double)totalBytesRemaining / (double)totalBytesToDownload) * 100);
                        bgWorker.ReportProgress(progressPercentage, "Downloading Missing Files");

                        DownloadMissingFilesClass.DownloadMissingFiles($"{ServerIP}:{Port}/Game", UserName, Password, DownloadFolderPath);
                        bgWorker.ReportProgress(100, "Missing Files Downloaded");
                    }
                    else
                    {
                        bgWorker.ReportProgress(100, "No Missing Files");
                    }

                    List<string> unnecessaryFiles = GetUnnecessaryLocalFilesClass.GetUnnecessaryLocalFiles(serverFiles, DownloadFolderPath, serverFolderUrl);

                    bgWorker.ReportProgress(100, "Cleaing Up Files");
                    DeleteUnnecessaryLocalFilesClass.DeleteUnnecessaryLocalFiles(unnecessaryFiles, DownloadFolderPath);

                    bgWorker.ReportProgress(100, "Checking Files");
                    CompareAndDownloadFilesClass.CompareAndDownloadFiles($"{ServerIP}:{Port}/Game", UserName, Password, DownloadFolderPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            percentageLabel.Text = $"{e.ProgressPercentage}%";
            progressBar.Value = e.ProgressPercentage;
            statusLabel.Text = e.UserState.ToString();
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            percentageLabel.Text = "100%";
            progressBar.Value = 100;

            if (e.Error != null)
            {
                MessageBox.Show($"Error occurred: {e.Error.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (e.Cancelled)
            {
                MessageBox.Show("Download canceled.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                statusLabel.Text = "Download/Update Complete";
            }

            bgWorker.Dispose();

            playBtn.Enabled = true;
            updateBtn.Enabled = true;
            settingsBtn.Enabled = true;
        }

        private void playBtn_Click(object sender, EventArgs e)
        {
            string exePath = $"{DownloadFolderPath}/Poro King.exe";

            if (File.Exists(exePath))
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = exePath;
                Process.Start(startInfo);
            }
            else
            {
                MessageBox.Show("Executable file not found at the specified location.");
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            LoadSettings();

            if (!bgWorker.IsBusy)
            {
                if (!Directory.Exists(DownloadFolderPath))
                {
                    Directory.CreateDirectory(DownloadFolderPath);
                }

                percentageLabel.Text = "0%";
                progressBar.Value = 0;
                statusLabel.Text = "Starting";
                bgWorker.RunWorkerAsync();
            }
            else
            {
                MessageBox.Show("A download operation is already in progress.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            updateBtn.Enabled = false;
            settingsBtn.Enabled = false;
        }

        private void settingsBtn_Click(object sender, EventArgs e)
        {
            if (File.Exists(SettingsPath))
            {
                Process.Start("notepad.exe", SettingsPath);
            }
        }
    }
}