using System;
using System.IO;
using Windows.Storage;

using Xamarin.Forms;
using ToDo.UWP;
using ToDoPCL.Interfaces;


[assembly: Dependency(typeof(FileHelper))]
namespace ToDo.UWP
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            return Path.Combine(ApplicationData.Current.LocalFolder.Path, filename);
        }
    }
}
