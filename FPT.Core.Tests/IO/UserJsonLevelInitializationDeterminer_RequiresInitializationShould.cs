using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using FPT.Core.IO;
using System.IO;
namespace FPT.Core.Tests.IO
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
    }
}
