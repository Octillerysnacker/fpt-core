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
        public bool IsMarkedAsInitialized { get; private set; }
        public MockLevelInitializationDeterminer(bool shouldInitialize, bool isMarkedAsInitialized = false)
        {
            ShouldInitialize = shouldInitialize;
            IsMarkedAsInitialized = isMarkedAsInitialized;
        }
        public bool RequiresInitialization(string userFolderPath)
        {
            SuppliedUserFolderPath = userFolderPath;
            return ShouldInitialize;
        }

        public void MarkAsInitialized(string userFolderPath)
        {
            SuppliedUserFolderPath = userFolderPath;
            IsMarkedAsInitialized = true;
        }

        public void UnmarkAsInitialized(string userFolderPath)
        {
            SuppliedUserFolderPath = userFolderPath;
            IsMarkedAsInitialized = false;
        }
    }
}
