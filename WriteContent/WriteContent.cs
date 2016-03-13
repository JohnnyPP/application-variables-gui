using System;
using System.IO;

namespace JobApplication
{
	/// <summary>
	/// Write file content.
	/// </summary>
	public class WriteFileContent
	{
		string _pathShell;

		public WriteFileContent (string pathShell)
		{
			_pathShell = pathShell;
		}

		/// <summary>
		/// Writes the content.
		/// </summary>
		/// <param name="content">Content.</param>
		/// <param name="fileName">File name.</param>
		public void WriteContent(string content, string fileName)
		{
			using (StreamWriter outfile = new StreamWriter(_pathShell + fileName))
			{
				outfile.Write(content);
			}	
		}

		/// <summary>
		/// Writes the salary.
		/// </summary>
		/// <param name="content">Content.</param>
		/// <param name="checkButton">If set to <c>true</c> check button.</param>
		/// <param name="fileName">File name.</param>
		public void WriteSalary(string content, bool checkButton)
		{
			using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterSalary.txt"))
			{
				if (checkButton) 
				{
					outfile.Write("\\ Meine Gehaltsvorstellungen liegen zwischen " + content + " Euro brutto im Jahr.");
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
		/// <param name="content">Content.</param>
		/// <param name="checkButton">If set to <c>true</c> check button.</param>
		public void WriteSalaryHelper(string content, bool checkButton)
		{
			if (checkButton) 
			{
				using (StreamWriter outfile = new StreamWriter(_pathShell + "helperSalary.txt"))
				{

					outfile.Write(content);
				}
			}
		}

		/// <summary>
		/// Writes the position.
		/// </summary>
		/// <param name="contentJobNumber">Content job number.</param>
		/// <param name="contentJobPosition">Content job position.</param>
		public void WritePosition(string contentJobNumber, string contentJobPosition)
		{
			using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterPosition.txt"))
			{
				outfile.Write("Bewerbung auf die Stelle " + contentJobNumber + " als " + contentJobPosition);
			}
		}

		/// <summary>
		/// Writes the opening.
		/// </summary>
		/// <param name="comboActive">If set to <c>true</c> combo active.</param>
		/// <param name="comboText">Combo text.</param>
		/// <param name="content">Content.</param>
		public void WriteOpening(int comboActive, string comboText, string content)
		{
			string temp; 

			using (StreamWriter outfile = new StreamWriter(_pathShell + "coverLetterOpening.txt"))
			{
				if (comboActive == 0) 
				{
					temp = comboText + ",";
				} 
				else 
				{
					temp = comboText + " " + content + ",";
				}

				outfile.Write (temp);
			}
		}
	}
}

