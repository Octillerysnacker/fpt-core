﻿using System;
using System.IO.Abstractions;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FPT.Core.Levels.Initialization
{
    public class UserJsonLevelInitializationDeterminer : ILevelInitializationDeterminer
    {
        private IFileSystem fileSystem;
        private IDirectory directory;
        private string userJsonFileName = "user.json";
        public UserJsonLevelInitializationDeterminer(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            directory = fileSystem.Directory;
        }

        public void MarkAsInitialized(string userFolderPath)
        {
            directory.CreateDirectory(userFolderPath);
            fileSystem.File.Create(fileSystem.Path.Combine(userFolderPath, userJsonFileName));
        }

        public bool RequiresInitialization(string userFolderPath)
        {
            if (!directory.Exists(userFolderPath))
            {
                return true;
            }
            else
            {
                int amountOfUserJsonFiles = fileSystem.Directory.EnumerateFiles(userFolderPath, userJsonFileName).Count();
                return amountOfUserJsonFiles != 1;
            }
        }

        public void UnmarkAsInitialized(string userFOlderPath)
        {
            fileSystem.File.Delete(fileSystem.Path.Combine(userFOlderPath, userJsonFileName));
        }
    }
}
