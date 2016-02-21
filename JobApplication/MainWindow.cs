using System;
using System.IO;
using Gtk;

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
	/// The path variables.
	/// </summary>
	string _pathVariables;

	/// <summary>
	/// Initializes a new instance of the <see cref="MainWindow"/> class.
	/// </summary>
	/// <param name="pathShell">Path shell.</param>
	public MainWindow (string pathShell) : base (Gtk.WindowType.Toplevel)
	{
		_pathShell = pathShell + "/variables/";
		Build ();
		Initialization ();
	}

	/// <summary>
	/// Initalizes the entries with the content of the files from variables folder
	/// </summary>
	private void Initialization()
	{
		ReadJobPosition ();
		ReadOpening ();
		ReadSalary ();
		JobNumber.Text = ReadContent("coverLetterCodeNumber.txt").TrimStart('\\').Trim();
		Corporation.Text = ReadContent ("coverLetterRecipientFirstLine.txt");
		Email.Text = ReadContent ("eMailAddress.txt");
		Recruiting.Text = ReadContent ("helperRecipientSecondLine0.txt");
		Person.Text = ReadContent ("helperRecipientSecondLine1.txt");
		Street.Text = ReadContent ("helperRecipientSecondLine2.txt");
		City.Text = ReadContent ("helperRecipientSecondLine3.txt");
	}

	/// <summary>
	/// Raises the create application clicked event to write contents of the entries to the files
	/// </summary>
	/// <param name="sender">Sender.</param>
	/// <param name="e">E.</param>
	protected void OnCreateApplicationClicked (object sender, EventArgs e)
	{
		WriteOpening ();
		WriteCodeNumber ();
		WritePosition ();
		WriteSalary ();
		WriteSalaryHelper ();
		CoverLetterRecipientSecondLine ();
		WriteContent (Email.Text, "eMailAddress.txt");
		WriteContent (Corporation.Text, "coverLetterRecipientFirstLine.txt");

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
			Salary.Text = ReadContent ("helperSalary.txt").Trim();
		} 
		else 
		{
			Salary.Text = "";
		}
	}

	/// <summary>
	/// Reads the content.
	/// </summary>
	/// <returns>The content.</returns>
	/// <param name="fileName">File name.</param>
	private string ReadContent (string fileName)
	{
		_pathVariables = _pathShell + fileName;
		return System.IO.File.ReadAllText(_pathVariables);
	}

	/// <summary>
	/// Writes the content.
	/// </summary>
	/// <param name="entry">Entry.</param>
	/// <param name="fileName">File name.</param>
	private void WriteContent (string entry, string fileName)
	{
		using (StreamWriter outfile = new StreamWriter(_pathShell + fileName))
		{
			outfile.Write(entry);
		}
	}

	/// <summary>
	/// Reads the job position.
	/// </summary>
	private void ReadJobPosition()
	{
		_pathVariables = _pathShell + "coverLetterPosition.txt";
		string coverLetterJobPosition = System.IO.File.ReadAllText(_pathVariables);
		string[] stringSeparators = new string[] {"Bewerbung auf die Stelle "," als"};
		string[] jobCodePosition = coverLetterJobPosition.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
		JobPosition.Text = jobCodePosition [1].Trim();
	}

	/// <summary>
	/// Reads the opening.
	/// </summary>
	private void ReadOpening()
	{
		char[] trimComma = {','};
		_pathVariables = _pathShell + "coverLetterOpening.txt";
		string coverLetterOpening = System.IO.File.ReadAllText(_pathVariables);

		if (coverLetterOpening.StartsWith ("Sehr geehrte Damen und Herren")) 
		{
			LetterOpeningPerson.Text = "";
			LetterOpeningCombo.Active = 0;
		}
		if (coverLetterOpening.StartsWith ("Sehr geehrte Frau")) 
		{
			int index = coverLetterOpening.IndexOf("Sehr geehrte Frau");
			string coverLetterOpeningWoman = (index < 0)
				? coverLetterOpening
				: coverLetterOpening.Remove(index, "Sehr geehrte Frau".Length);

			LetterOpeningPerson.Text = coverLetterOpeningWoman.Trim().TrimEnd(trimComma);
			LetterOpeningCombo.Active = 1;
		}
		if (coverLetterOpening.StartsWith ("Sehr geehrter Herr")) 
		{
			int index = coverLetterOpening.IndexOf("Sehr geehrter Herr");
			string coverLetterOpeningMan = (index < 0)
				? coverLetterOpening
				: coverLetterOpening.Remove(index, "Sehr geehrter Herr".Length);

			LetterOpeningPerson.Text = coverLetterOpeningMan.Trim().TrimEnd(trimComma);
			LetterOpeningCombo.Active = 2;
		}
	}

	/// <summary>
	/// Reads the salary.
	/// </summary>
	private void ReadSalary()
	{
		_pathVariables = _pathShell + "coverLetterSalary.txt";
		string coverLetterSalary = System.IO.File.ReadAllText(_pathVariables);
		if (coverLetterSalary == "\\")
		{
			Salary.Text = "";
			checkButtonSalary.Active = false;
		}
		else 
		{
			ReadContent ("helperSalary.txt");
			checkButtonSalary.Active = true;
		}
	}

	/// <summary>
	/// Writes coverLetterSalary.txt
	/// </summary>
	private void WriteSalary()
	{
		using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterSalary.txt"))
		{
			if (checkButtonSalary.Active) 
			{
				outfile.Write("\\ Meine Gehaltsvorstellungen liegen zwischen " + Salary.Text + " Euro brutto im Jahr.");
			} 
			else 
			{
				outfile.Write("\\");
			}
		}
	}

	/// <summary>
	/// Writes the salary helper.
	/// </summary>
	private void WriteSalaryHelper()
	{
		if (checkButtonSalary.Active) 
		{
			using (StreamWriter outfile = new StreamWriter(_pathShell + "helperSalary.txt"))
			{

				outfile.Write(Salary.Text);
			}
		}
	}

	/// <summary>
	/// Writes coverLetterPosition.txt
	/// </summary>
	private void WritePosition()
	{
		using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterPosition.txt"))
		{
			outfile.Write("Bewerbung auf die Stelle " + JobNumber.Text + " als " + JobPosition.Text);
		}
	}

	/// <summary>
	/// Writes coverLetterOpening.txt
	/// </summary>
	private void WriteOpening()
	{
		using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterOpening.txt"))
		{
			string temp = LetterOpeningCombo.ActiveText + " " + LetterOpeningPerson.Text;
			outfile.Write(temp);
		}
	}

	/// <summary>
	/// Writes coverLetterCodeNumber.txt
	/// </summary>
	private void WriteCodeNumber()
	{
		using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterCodeNumber.txt"))
		{
			outfile.Write("\\ " + JobNumber.Text);
		}
	}

	/// <summary>
	/// Writes contents of the entries to the coverLetterRecipientSecondLine.txt file.
	/// coverLetterRecipientSecondLine.txt file follows the pattern: "Recruiting\\Person\\Street\\City"
	/// e.g. "Personalabteilung\\Karin Deckert\\Bahnhofstrasse\\Oberkochen"
	/// where "\\" is the new line escape character in Latex 
	/// </summary>
	private void CoverLetterRecipientSecondLine()
	{
		Entry[] secondLineEntries = new[] 
		{
			Recruiting,
			Person,
			Street,
			City
		};

		string[] secondLineFiles = new[] 
		{
			"helperRecipientSecondLine0.txt",
			"helperRecipientSecondLine1.txt",
			"helperRecipientSecondLine2.txt",
			"helperRecipientSecondLine3.txt"
		};

		for (int i = 0; i < 4; i++) 
		{			
			WriteContent (secondLineEntries [i].Text, secondLineFiles [i]);
		}

		System.Text.StringBuilder secondLineAppended = new System.Text.StringBuilder();

		foreach (var entry in secondLineEntries)
		{
			string tempEntry = entry.Text;

			if (tempEntry.Equals ("")) 
			{
				secondLineAppended.Append(tempEntry);
			}
			else 
			{
				secondLineAppended.Append(tempEntry + "\\\\");
			}
		}
		string appendedString = secondLineAppended.ToString().TrimEnd('\\');

		using (var writeToFile = new StreamWriter(_pathShell + "coverLetterRecipientSecondLine.txt"))
		{
			writeToFile.Write(appendedString);
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
