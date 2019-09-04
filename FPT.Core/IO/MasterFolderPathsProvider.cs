using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
namespace FPT.Core.IO
{
    public static class MasterFolderPathsProvider
    {
        public static string GetMasterFolder(string levelFolderFilepath)
        {
            return Path.Combine(levelFolderFilepath, "master");
        }
    }
}
