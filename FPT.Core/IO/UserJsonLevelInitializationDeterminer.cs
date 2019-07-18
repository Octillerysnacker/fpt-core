using System;
using System.IO.Abstractions;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace FPT.Core.IO
{
    public class UserJsonLevelInitializationDeterminer : ILevelInitializationDeterminer
    {
        private IFileSystem fileSystem;
        private string userJsonFileName = "user.json";
        public UserJsonLevelInitializationDeterminer(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        public void MarkAsInitialized(string userFolderPath)
        {
            fileSystem.File.Create(fileSystem.Path.Combine(userFolderPath, userJsonFileName));
        }

        public bool RequiresInitialization(string userFolderPath)
        {
            //TODO: Make this more clear
            return fileSystem.Directory.EnumerateFiles(userFolderPath, userJsonFileName).Count() != 1;
        }
    }
}
