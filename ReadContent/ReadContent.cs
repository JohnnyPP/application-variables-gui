using System;

namespace JobApplication
{
	/// <summary>
	/// Read file content.
	/// </summary>
	public class ReadFileContent
	{
		/// <summary>
		/// The path shell.
		/// </summary>
		string _pathShell;
		/// <summary>
		/// Initializes a new instance of the <see cref="JobApplication.ReadFileContent"/> class.
		/// </summary>
		/// <param name="shellPath">Shell path.</param>
		public ReadFileContent (string shellPath)
		{
			_pathShell = shellPath;
		}

		/// <summary>
		/// Reads the content.
		/// </summary>
		/// <returns>The content.</returns>
		/// <param name="fileName">File name.</param>
		public string ReadContent(string fileName)
		{
			return System.IO.File.ReadAllText(_pathShell + fileName);
		}

		/// <summary>
		/// Reads the job position.
		/// </summary>
		/// <returns>The job position.</returns>
		public string ReadJobPosition()
		{
			string coverLetterJobPosition = ReadContent ("coverLetterPosition.txt");
			string[] stringSeparators = new string[] {"Bewerbung auf die Stelle "," als"};
			string[] jobCodePosition = coverLetterJobPosition.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

			if (jobCodePosition.Length == 2) 
			{
				return jobCodePosition [1].Trim ();
			} 
			else 
			{	
				return jobCodePosition [0].Trim ();;
			}
		}

		/// <summary>
		/// Reads the salary.
		/// </summary>
		public bool ReadSalary()
		{
			string coverLetterSalary = ReadContent ("coverLetterSalary.txt");
			if (coverLetterSalary == "\\")
			{
				return false;
			}
			else 
			{
				return true;
			}
		}

		/// <summary>
		/// Reads the opening.
		/// </summary>
		/// <returns>The opening.</returns>
		public Tuple<string,int> ReadOpening()
		{
			char[] trimComma = {','};
			string coverLetterOpening = ReadContent("coverLetterOpening.txt");

			if (coverLetterOpening.StartsWith ("Sehr geehrte Damen und Herren")) 
			{
				return Tuple.Create ("", 0);
			} 
			else if (coverLetterOpening.StartsWith ("Sehr geehrte Frau")) 
			{
				int index = coverLetterOpening.IndexOf ("Sehr geehrte Frau");
				string coverLetterOpeningWoman = (index < 0)
					? coverLetterOpening
					: coverLetterOpening.Remove (index, "Sehr geehrte Frau".Length);

				return Tuple.Create (coverLetterOpeningWoman.Trim ().TrimEnd (trimComma), 1);
			} 
			else if (coverLetterOpening.StartsWith ("Sehr geehrter Herr")) 
			{
				int index = coverLetterOpening.IndexOf ("Sehr geehrter Herr");
				string coverLetterOpeningMan = (index < 0)
					? coverLetterOpening
					: coverLetterOpening.Remove (index, "Sehr geehrter Herr".Length);

				return Tuple.Create (coverLetterOpeningMan.Trim ().TrimEnd (trimComma), 2);
			} 
			else 
			{
				return Tuple.Create ("Error", 3);
			}
		}
	}
}

