using System;
using Gtk;

namespace JobApplication
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			// in initalization load the previous values from the variables folder
			// modify
			// press button and update variables

			Application.Init ();

			//MainWindow win = new MainWindow (args[0]);
			MainWindow win = new MainWindow ("/media/ntfs2/FirefoxProfile/zotero/storage/WJACEEU9");
			win.Show ();
			Application.Run ();
		}
	}
}
