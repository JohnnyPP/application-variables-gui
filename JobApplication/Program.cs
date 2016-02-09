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
			MainWindow win = new MainWindow ();
			win.Show ();
			Application.Run ();
		}
	}
}
