using System.IO;
using HSNP.Droid.Helpers;
using HSNP.Interface;
using HSNP.Interfaces;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(FileHelper))]
namespace HSNP.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}