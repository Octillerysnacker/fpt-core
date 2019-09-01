using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Levels.Initialization;

namespace FPT.Core.Tests.Levels.Initialization
{
    public class MockLevelInitializationDeterminer : ILevelInitializationDeterminer
    {
        public bool ShouldInitialize { get; }
        public string SuppliedUserFolderPath { get; private set; }
        public bool IsMarkedAsInitialized { get; set; }
        public MockLevelInitializationDeterminer(bool shouldInitialize)
        {
            ShouldInitialize = shouldInitialize;
        }
        public bool RequiresInitialization(string userFolderPath)
        {
            SuppliedUserFolderPath = userFolderPath;
            return ShouldInitialize;
        }

        public void MarkAsInitialized(string userFolderPath)
        {
            IsMarkedAsInitialized = true;
        }
    }
}
