using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace FPT.Core.IO
{
    public static class LevelPathsProvider
    {
        public static string GetProjectFolder(string levelFolderPath,string user)
        {
            return Path.Combine(GetUserFolder(levelFolderPath,user), "project");
        }
        public static string GetUserFolder(string levelFolderPath, string user)
        {
            return Path.Combine(levelFolderPath, "users", user);
        }
    }
}
