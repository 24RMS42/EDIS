using System;
using EDIS.Core;

namespace EDIS.iOS.Provider
{
    public class FileAccessHelper : IDbPath
    {
        public string GetLocalDbPath()
        {
            const string db = "edismobile.db3";
            var docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libFolder = System.IO.Path.Combine(docFolder, "..", "Library");
            var path = System.IO.Path.Combine(libFolder, "EDISMobile");
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            path = System.IO.Path.Combine(path, db);
            return path;
        }
    }
}