using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.ComponentModel;
using System.Threading;

namespace INVedit
{
	public partial class UpdateForm : Form
	{
		List<string> download;
		List<string> args;
		WebClient client;
		bool cancel = false;
		bool close = true;
		
		public UpdateForm(List<string> download)
		{
			this.download = download;
			InitializeComponent();
		}
		
		public void Start()
		{
			Uri uri = new Uri(download[0]);
			string local = uri.Segments[uri.Segments.Length-1];
			barProgress.Maximum = download.Count*100;
			boxProgress.Text = "Progress: "+local;
			args = new List<string>();
			Show();
			client = new WebClient();
			client.DownloadProgressChanged += delegate(object sender, DownloadProgressChangedEventArgs e) {
				barProgress.Value = args.Count*100 + e.ProgressPercentage;
			};
			client.DownloadFileCompleted += delegate(object sender, AsyncCompletedEventArgs e) {
				if (cancel) return;
				if (e.Cancelled) {
					if (MessageBox.Show(e.Error.ToString()+"\nContinue?", "Error",
					                    MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.Cancel) {
						foreach (string file in args) File.Delete(file);
						return;
					}
				} else {
					args.Add(local);
					barProgress.Value = args.Count*100;
				}
				download.RemoveAt(0);
				if (download.Count > 0) {
					uri = new Uri(download[0]);
					local = uri.Segments[uri.Segments.Length-1];
					Thread.Sleep(100);
					boxProgress.Text = "Progress: "+local;
					client.DownloadFileAsync(uri, "_"+local);
				} else {
					barProgress.Text = "Progress: Finished.";
					btnFinish.Enabled = true;
				}
			};
			client.DownloadFileAsync(uri, "_"+local);
		}
		
		void BtnFinishClick(object sender, EventArgs e)
		{
			args.Insert(0, "-update");
			string arguments = string.Join(" ", args.ToArray());
			if (args.Contains("INVedit.exe")) Process.Start("_INVedit.exe", arguments);
			else Process.Start(Application.ExecutablePath, arguments);
			close = false;
			Application.Exit();
		}
		
		void BtnCancelClick(object sender, EventArgs e)
		{
			Close();
		}
		
		void UpdateFormFormClosing(object sender, FormClosingEventArgs e)
		{
			if (!close) return;
			if (download.Count > 0) {
				cancel = true;
				client.CancelAsync();
				client.Dispose();
				Uri uri = new Uri(download[0]);
				string local = uri.Segments[uri.Segments.Length-1];
				File.Delete("_"+local);
			} else if (MessageBox.Show("INVedit already finished downloading the update.\nDo you really want to cancel it?",
			                           "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) {
				e.Cancel = true;
				return;
			}
			foreach (string file in args) File.Delete("_"+file);
			close = false;
			Application.Exit();
		}
	}
}
