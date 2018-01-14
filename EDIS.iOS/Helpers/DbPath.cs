using System;
using EDIS.Core;
using EDIS.iOS.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbPath))]
namespace EDIS.iOS.Helpers
{
    public class DbPath : IDbPath
    {
        public string GetLocalDbPath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = System.IO.Path.Combine(docFolder, "..", "Library");

            if (!System.IO.Directory.Exists(libFolder))
            {
                System.IO.Directory.CreateDirectory(libFolder);
            }

            return System.IO.Path.Combine(libFolder, "scan2move.db3");
        }
    }
}