using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using System.IO;
using FPT.Core.Levels.Initialization;
using FPT.Core.Tests.IO;

namespace FPT.Core.Tests.Levels.Initialization
{
    public class UserJsonLevelInitializationDeterminer_MarkAsInitializedShould
    {
        [Theory]
        [ClassData(typeof(RandomPathsDataSet))]
        public void CreateJsonFileInUserFolder(string userFolder)
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(userFolder);
            var determiner = new UserJsonLevelInitializationDeterminer(mockFileSystem);
            var jsonFilePath = mockFileSystem.Path.Combine(userFolder, "user.json");

            determiner.MarkAsInitialized(userFolder);

            Assert.True(mockFileSystem.FileExists(jsonFilePath));
        }
        [Theory]
        [ClassData(typeof(RandomPathsDataSet))]
        public void CreateUserFolderIfDoesNotExist(string userFolder)
        {
            var mockFileSystem = new MockFileSystem();
            var determiner = new UserJsonLevelInitializationDeterminer(mockFileSystem);
            var jsonFilePath = mockFileSystem.Path.Combine(userFolder, "user.json");

            determiner.MarkAsInitialized(userFolder);

            Assert.True(mockFileSystem.FileExists(jsonFilePath));
        }
    }
}
