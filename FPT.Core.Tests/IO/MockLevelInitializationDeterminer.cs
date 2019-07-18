using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.IO;
namespace FPT.Core.Tests.IO
{
    public class MockLevelInitializationDeterminer : ILevelInitializationDeterminer
    {
        public bool ShouldInitialize { get; }
        public string SuppliedLevelId { get; private set; }
        public string SuppliedUser { get; private set; }
        public MockLevelInitializationDeterminer(bool shouldInitialize)
        {
            ShouldInitialize = shouldInitialize;
        }

        public bool RequiresInitialization(string levelId, string user)
        {
            SuppliedLevelId = levelId;
            SuppliedUser = user;
            return ShouldInitialize;
        }
    }
}
