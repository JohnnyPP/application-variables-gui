using System;
using Gtk;

namespace JobApplication
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();

			//MainWindow win = new MainWindow (args[0]);
			MainWindow win = new MainWindow ("/media/ntfs2/FirefoxProfile/zotero/storage/WJACEEU9");
			win.Show ();
			Application.Run ();
		}
	}
}
