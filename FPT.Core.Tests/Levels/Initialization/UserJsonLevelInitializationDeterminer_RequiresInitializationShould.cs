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
    public class UserJsonLevelInitializationDeterminer_RequiresInitializationShould
    {
        [Theory]
        [ClassData(typeof(RandomPathsDataSet))]
        public void ReturnFalseWhenJsonFileFound(string userFolder)
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddFile(Path.Combine(userFolder, "user.json"), new MockFileData(""));
            var determiner = new UserJsonLevelInitializationDeterminer(mockFileSystem);

            var result = determiner.RequiresInitialization(userFolder);

            Assert.False(result);
        }
        [Theory]
        [ClassData(typeof(RandomPathsDataSet))]
        public void ReturnTrueWhenNoJsonFileFound(string userFolder)
        {
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(userFolder);
            var determiner = new UserJsonLevelInitializationDeterminer(mockFileSystem);

            var result = determiner.RequiresInitialization(userFolder);

            Assert.True(result);
        }
        [Theory]
        [ClassData(typeof(RandomPathsDataSet))]
        public void ReturnTrueWhenUserFolderDoesNotExist(string userFolder)
        {
            var mockFileSystem = new MockFileSystem();
            var determiner = new UserJsonLevelInitializationDeterminer(mockFileSystem);

            var result = determiner.RequiresInitialization(userFolder);

            Assert.True(result);
        }
    }
}
