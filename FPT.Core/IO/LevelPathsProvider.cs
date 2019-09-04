using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using FPT.Core.Levels;

namespace FPT.Core.IO
{
    public static class LevelPathsProvider
    {
        private static string GetProjectFolder(string levelFolderPath,string user)
        {
            return Path.Combine(GetUserFolder(levelFolderPath,user), "project");
        }
        public static string GetProjectFolder(this Level level, string user)
        {
            return GetProjectFolder(level.FolderFilepath, user);
        }
        private static string GetUserFolder(string levelFolderPath, string user)
        {
            return Path.Combine(levelFolderPath, "users", user);
        }
        public static string GetUserFolder(this Level level, string user)
        {
            return GetUserFolder(level.FolderFilepath, user);
        }
        public static string GetInstructionsPath(this Level level)
        {
            return Path.Combine(level.FolderFilepath, level.InstructionsFilepath);
        }
        public static string GetVerifierFilepath(this Level level)
        {
            return Path.Combine(level.FolderFilepath, level.VerifierFilepath);
        }
    }
}
