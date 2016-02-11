﻿using System;
using System.IO;
using Gtk;

public partial class MainWindow: Gtk.Window
{
	string _pathShell;
	string _pathVariables;
	string _personOpeningLetter;

	public MainWindow (string pathShell) : base (Gtk.WindowType.Toplevel)
	{
		_pathShell = pathShell + "/variables/";

		// media/ntfs2/FirefoxProfile/zotero/storage/WJACEEU9
		// mono JobApplication.exe /media/ntfs2/FirefoxProfile/zotero/storage/WJACEEU9
		Build ();
		Initialization ();
	}

	private void Initialization()
	{
		char[] trimCoverLetterJobNumber = { '\\', ' ', ',' };
		char[] trimComma = {','};
		//char[] trimCoverLetterOpening = { "Sehr geehrte Frau", "Sehr geehrter Herr"};

		_pathVariables = _pathShell + "coverLetterCodeNumber.txt";
		string coverLetterJobNumber = System.IO.File.ReadAllText(_pathVariables);
		JobNumber.Text = coverLetterJobNumber.TrimStart (trimCoverLetterJobNumber);

		_pathVariables = _pathShell + "coverLetterPosition.txt";
		string coverLetterJobPosition = System.IO.File.ReadAllText(_pathVariables);
		string[] stringSeparators = new string[] {"Bewerbung auf die Stelle "," als"};
		string[] jobCodePosition = coverLetterJobPosition.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
		JobPosition.Text = jobCodePosition [1].Trim();


		_pathVariables = _pathShell + "coverLetterOpening.txt";
		string coverLetterOpening = System.IO.File.ReadAllText(_pathVariables);

		if (coverLetterOpening.StartsWith ("Sehr geehrte Damen und Herren")) 
		{
			// make combo swith to "Sehr geehrte Damen und Herren"
			_personOpeningLetter = LetterOpeningPerson.Text = "";
			LetterOpeningCombo.Active = 0;
		}
		if (coverLetterOpening.StartsWith ("Sehr geehrte Frau")) 
		{
			int index = coverLetterOpening.IndexOf("Sehr geehrte Frau");
			string coverLetterOpeningWoman = (index < 0)
				? coverLetterOpening
				: coverLetterOpening.Remove(index, "Sehr geehrte Frau".Length);
			
			_personOpeningLetter = LetterOpeningPerson.Text = coverLetterOpeningWoman.Trim().TrimEnd(trimComma);
			LetterOpeningCombo.Active = 1;
		}
		if (coverLetterOpening.StartsWith ("Sehr geehrter Herr")) 
		{
			int index = coverLetterOpening.IndexOf("Sehr geehrter Herr");
			string coverLetterOpeningMan = (index < 0)
				? coverLetterOpening
				: coverLetterOpening.Remove(index, "Sehr geehrter Herr".Length);
			
			_personOpeningLetter = LetterOpeningPerson.Text = coverLetterOpeningMan.Trim().TrimEnd(trimComma);
			LetterOpeningCombo.Active = 2;
		}

		_pathVariables = _pathShell + "coverLetterRecipientFirstLine.txt";
		string coverLetterCorporation = System.IO.File.ReadAllText(_pathVariables);
		Corporation.Text = coverLetterCorporation;





		_pathVariables = _pathShell + "eMailAddress.txt";
		string coverLetterEmail = System.IO.File.ReadAllText(_pathVariables);
		Email.Text = coverLetterEmail;


		
	}

	protected void OnDeleteEvent (object sender, DeleteEventArgs a)
	{
		Application.Quit ();
		a.RetVal = true;
	}

	protected void OnButton1Clicked (object sender, EventArgs e)
	{
		

		//
		// Writes coverLetterOpening.txt
		//
		using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterOpening.txt"))
		{
			string temp = LetterOpeningCombo.ActiveText + " " + LetterOpeningPerson.Text;
			outfile.Write(temp);
		}

		//
		// Writes eMailAddress.txt
		//
		using (StreamWriter outfile = new StreamWriter(_pathShell + "eMailAddress.txt"))
		{
			outfile.Write(Email.Text);
		}

		//
		// Writes coverLetterCodeNumber.txt
		//
		using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterCodeNumber.txt"))
		{
			outfile.Write("\\ " + JobNumber.Text);
		}


		using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterPosition.txt"))
		{
			outfile.Write("Bewerbung auf die Stelle " + JobNumber.Text + " als " + JobPosition.Text);
		}

	



	}
}
