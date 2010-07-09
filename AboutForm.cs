using System;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;

namespace INVedit
{
	public partial class AboutForm : Form
	{
		public AboutForm()
		{
			InitializeComponent();
			label1.Text = label1.Text.Replace("{version}", Assembly.GetExecutingAssembly().GetName().Version.ToString(3));
			label1.Text = label1.Text.Replace("{net-version}", Environment.Version.ToString());
		}
		
		void LinkLabel1LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.icsharpcode.net/OpenSource/SD/");
		}
		void LinkLabel2LinkClicked(object sender,LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.minecraftforum.net/viewtopic.php?t=15921");
		}
		void LinkLabel3LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.famfamfam.com/lab/icons/silk/");
		}
	}
}
