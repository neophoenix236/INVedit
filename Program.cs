using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace INVedit
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			if (args.Length > 0) {
				if (args[0] == "-update") {
					Thread.Sleep(100);
					bool finish = false;
					for (int i = 1; i < args.Length; ++i) {
						if (args[i] == "INVedit.exe") finish = true;
						else {
							if (File.Exists(args[i])) File.Delete(args[i]);
							File.Move("_"+args[i], args[i]);
						}
					}
					if (finish) {
						File.Delete("INVedit.exe");
						File.Copy("_INVedit.exe", "INVedit.exe");
						Process.Start("INVedit.exe", "-finish");
						return;
					}
					args = new string[0];
				} else if (args[0] == "-finish") {
					Thread.Sleep(100);
					File.Delete("_INVedit.exe");
					args = new string[0];
				}
			}
			
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm(args));
		}
	}
}
