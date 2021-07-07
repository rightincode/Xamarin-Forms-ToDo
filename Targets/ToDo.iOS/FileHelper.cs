using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using ToDo.iOS;
using ToDoPCL.Data;

[assembly: Dependency(typeof(FileHelper))]
namespace ToDo.iOS
{
	public class FileHelper : IFileHelper
	{
		public string GetLocalFilePath(string filename)
		{
			string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			return Path.Combine(path, filename);
		}
	}
}
