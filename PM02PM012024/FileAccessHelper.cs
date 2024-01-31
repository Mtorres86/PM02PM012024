using System;
namespace PM02PM012024
{
	public class FileAccessHelper
	{
		public static string GetLocationPathFile(string filename)
		{
			return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);

		}
	}
}

