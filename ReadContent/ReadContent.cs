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
	}
}

