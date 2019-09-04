using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FPT.Core.Levels;

namespace FPT.Core.IO
{
    public static class MasterFolderPathsProvider
    {
        private static string GetMasterFolder(string levelFolderFilepath)
        {
            return Path.Combine(levelFolderFilepath, "master");
        }
        public static string GetMasterFolder(this Level level)
        {
            return GetMasterFolder(level.FolderFilepath);
        }
    }
}
