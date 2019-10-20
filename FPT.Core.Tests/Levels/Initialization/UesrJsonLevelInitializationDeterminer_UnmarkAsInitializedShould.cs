using FPT.Core.Levels.Initialization;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Xunit;

namespace FPT.Core.Tests.Levels.Initialization
{
    public class UesrJsonLevelInitializationDeterminer_UnmarkAsInitializedShould
    {
        [Theory]
        [ClassData(typeof(RemoveJsonFile_Data))]
        public void RemoveJsonFile(MockFileSystem fs, string userFolder)
        {
            var determiner = new UserJsonLevelInitializationDeterminer(fs);

            determiner.UnmarkAsInitialized(userFolder);

            Assert.False(fs.FileExists(fs.Path.Combine(userFolder, "user.json")));
        }
        private class RemoveJsonFile_Data : TheoryData <MockFileSystem, string>
        {
            private int datasetCount = 4;
            public RemoveJsonFile_Data()
            {
                for(int i = 0; i < datasetCount; i++)
                {
                    var fs = new MockFileSystem();
                    var userFolder = fs.Path.GetRandomFileName();
                    fs.AddFile(fs.Path.Combine(userFolder, "user.json"),new MockFileData(""));
                    Add(fs, userFolder);
                }
            }
        }
        [Fact]
        public void DoesNotThrowWhenUserJsonDoesNotExist()
        {
            var fs = new MockFileSystem();
            var userFolder = "user";
            fs.AddDirectory(userFolder);
            var determiner = new UserJsonLevelInitializationDeterminer(fs);

            determiner.UnmarkAsInitialized(userFolder);
        }
    }
}
