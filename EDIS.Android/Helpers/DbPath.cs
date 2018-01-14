using System;
using EDIS.Core;
using EDIS.Droid.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(DbPath))]
namespace EDIS.Droid.Helpers
{
    public class DbPath : IDbPath
    {
        public string GetLocalDbPath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return System.IO.Path.Combine(path, "scan2move.db3");
        }
    }
}