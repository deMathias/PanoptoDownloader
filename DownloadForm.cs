using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PanoptoDownloader
{
    public partial class DownloadForm : Form
    {
        private const string youtubeDlFilename = "youtube-dl.exe";
        private const string ffmpegFilename = "ffmpeg.exe";
        private const string commonArgument = "-o";

        public DownloadForm()
        {
            InitializeComponent();
        }

        private void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            // Handle the output data received from the process (if needed)
            if (!string.IsNullOrEmpty(e.Data))
            {
                // Do something with the output data
            }
        }

        private void Process_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            // Handle the error data received from the process (if needed)
            if (!string.IsNullOrEmpty(e.Data))
            {
                // Do something with the error data
            }
        }

        private void btnDownload_Click_1(object sender, EventArgs e)
        {
                        string url = txtUrl.Text;
            string outputName = txtOutputName.Text;

            if (!outputName.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase))
            {
                outputName += ".mp4";
            }

            string youtubeDlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, youtubeDlFilename);

            // Build the command
            string arguments = $"\"{url}\" {commonArgument} \"{outputName}\"";

            // Create a process start info
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = youtubeDlPath,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            // Start the process
            Process process = new Process { StartInfo = startInfo };
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_ErrorDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/deMathias/PanoptoDownloader");
        }
    }
}
