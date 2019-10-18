using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Levels.Initialization;

namespace FPT.Core.Tests.Levels.Initialization
{
    public class MockLevelInitializer : ILevelInitializer
    {
        public string InitializedLevelId { get; private set; }
        public string InitializedUser { get; private set; }
        public string CleanedLevelId { get; private set; }
        public string CleanedUser { get; private set; }

        public void Clean(string levelId, string user)
        {
            CleanedLevelId = levelId;
            CleanedUser = user;
        }

        public void InitializeIfNecessary(string levelId, string user)
        {
            InitializedLevelId = levelId;
            InitializedUser = user;
        }
    }
}
