using System;

namespace AppBancaEnLinea.Droid
{
    class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(docFolder, filename);
        }

        private FileAccessHelper()
        {

        }
    }
}