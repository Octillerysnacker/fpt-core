using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO.Abstractions;
namespace FPT.Core.IO
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
            var levelToInitialize = levelsProvider.GetLevels().Single(level => level.Id == levelId);
            var userFolder = path.Combine(levelToInitialize.FolderFilepath, user);
            if(levelInitializationDeterminer.RequiresInitialization(userFolder))
            {
                //TO-DO: Remove logical dependency on folder structure
                var masterFolder = path.Combine(levelToInitialize.FolderFilepath, "master");
                var projectFolder = path.Combine(userFolder, "project");

                copyDir.CopyAll(masterFolder, projectFolder);
            }
        }
    }
}
