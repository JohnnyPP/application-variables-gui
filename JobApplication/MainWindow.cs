using System;
using System.IO;
using Gtk;

namespace JobApplication
{
	/// <summary>
	/// Main window.
	/// </summary>
	public partial class MainWindow: Gtk.Window
	{
		/// <summary>
		/// The path shell.
		/// </summary>
		string _pathShell;
		/// <summary>
		/// The rfc.
		/// </summary>
		ReadFileContent _rfc;
		/// <summary>
		/// The wfc.
		/// </summary>
		WriteFileContent _wfc;

		/// <summary>
		/// Initializes a new instance of the <see cref="MainWindow"/> class.
		/// </summary>
		/// <param name="pathShell">Path shell.</param>
		public MainWindow (string pathShell) : base (Gtk.WindowType.Toplevel)
		{
			_pathShell = pathShell + "/variables/";
			_rfc = new ReadFileContent (_pathShell);
			_wfc = new WriteFileContent (_pathShell);
			Build ();
			Initialization ();
		}


		/// <summary>
		/// Initalizes the entries with the content of the files from variables folder
		/// </summary>
		private void Initialization()
		{
			JobPosition.Text = _rfc.ReadJobPosition ();
			LetterOpeningPerson.Text = _rfc.ReadOpening().Item1;
			LetterOpeningCombo.Active = _rfc.ReadOpening().Item2;
			checkButtonSalary.Active = _rfc.ReadSalary ();
			JobNumber.Text = _rfc.ReadContent("coverLetterCodeNumber.txt").TrimStart('\\').Trim();
			Corporation.Text = _rfc.ReadContent ("coverLetterRecipientFirstLine.txt");
			Email.Text = _rfc.ReadContent ("eMailAddress.txt");
			Recruiting.Text = _rfc.ReadContent ("helperRecipientSecondLine0.txt");
			Person.Text = _rfc.ReadContent ("helperRecipientSecondLine1.txt");
			Street.Text = _rfc.ReadContent ("helperRecipientSecondLine2.txt");
			City.Text = _rfc.ReadContent ("helperRecipientSecondLine3.txt");
		}

		/// <summary>
		/// Raises the create application clicked event to write contents of the entries to the files
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected void OnCreateApplicationClicked (object sender, EventArgs e)
		{
			_wfc.WriteOpening (LetterOpeningCombo.Active, LetterOpeningCombo.ActiveText, LetterOpeningPerson.Text);
			_wfc.WriteContent ("\\ " + JobNumber.Text, "coverLetterCodeNumber.txt");
			_wfc.WritePosition (JobNumber.Text, JobPosition.Text);
			_wfc.WriteSalary (Salary.Text, checkButtonSalary.Active);
			_wfc.WriteSalaryHelper (Salary.Text, checkButtonSalary.Active);
			_wfc.CoverLetterRecipientSecondLine (new[] {Recruiting.Text, Person.Text, Street.Text, City.Text});
			_wfc.WriteContent (Email.Text, "eMailAddress.txt");
			_wfc.WriteContent (Corporation.Text, "coverLetterRecipientFirstLine.txt");

			Application.Quit ();
		}

		/// <summary>
		/// Raises the delete event event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="a">The alpha component.</param>
		protected void OnDeleteEvent (object sender, DeleteEventArgs a)
		{
			Application.Quit ();
			a.RetVal = true;
		}

		/// <summary>
		/// Raises the check button salary clicked event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected void OnCheckButtonSalaryClicked (object sender, EventArgs e)
		{
			if (checkButtonSalary.Active == true) 
			{
				Salary.Text = _rfc.ReadContent ("helperSalary.txt").Trim();
			} 
			else 
			{
				Salary.Text = "";
			}
		}

		/// <summary>
		/// Raises the letter opening combo changed event.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		protected void OnLetterOpeningComboChanged (object sender, EventArgs e)
		{
			char[] split = new[] {' '};

			if (LetterOpeningCombo.Active == 0) 
			{
				LetterOpeningPerson.Text = "";
				Person.Text = "";
			}

			if (LetterOpeningCombo.Active == 1 || LetterOpeningCombo.Active == 2) 
			{
				string tempPerson = Person.Text;

				if (!tempPerson.Equals ("")) 
				{	
					string[] tempPersonArray = tempPerson.Split (split, 2);

					if (tempPersonArray.Length > 1) 
					{
						LetterOpeningPerson.Text = tempPersonArray [1].ToString ();
					}
				}
			}
		}
	}
}
