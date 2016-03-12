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
	}
}

