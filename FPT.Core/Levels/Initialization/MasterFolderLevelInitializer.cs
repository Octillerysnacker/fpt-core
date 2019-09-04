using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO.Abstractions;
using System.IO;
using FPT.Core.IO;
using FPT.Core.Levels.Providers;

namespace FPT.Core.Levels.Initialization
{
    public class MasterFolderLevelInitializer : ILevelInitializer
    {
        private ILevelInitializationDeterminer levelInitializationDeterminer;
        private ICopyDir copyDir;
        private ILevelsProvider levelsProvider;
        private IPath path;
        public MasterFolderLevelInitializer(ILevelInitializationDeterminer levelInitializationDeterminer,
                                            ICopyDir copyDir,
                                            ILevelsProvider levelsProvider,
                                            IPath path)
        {
            this.levelInitializationDeterminer = levelInitializationDeterminer;
            this.copyDir = copyDir;
            this.levelsProvider = levelsProvider;
            this.path = path;
        }
        public void InitializeIfNecessary(string levelId, string user)
        {
            var levelToInitialize = levelsProvider.GetLevel(levelId);
            var userFolder = levelToInitialize.GetUserFolder(user);
            if (levelInitializationDeterminer.RequiresInitialization(userFolder))
            {
                var masterFolder = levelToInitialize.GetMasterFolder();
                var projectFolder = levelToInitialize.GetProjectFolder(user);

                if (!path.FileSystem.Directory.Exists(masterFolder))
                {
                    throw new DirectoryNotFoundException($"Master folder for level {levelId} not found.");
                }
                else
                {
                    copyDir.CopyAll(masterFolder, projectFolder);

                    levelInitializationDeterminer.MarkAsInitialized(userFolder);
                }
            }
        }
    }
}
