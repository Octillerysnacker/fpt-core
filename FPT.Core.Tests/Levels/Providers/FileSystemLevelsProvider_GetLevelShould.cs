using System;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using FPT.Core.Exceptions;
using FPT.Core.Tests.Extensions;
using Newtonsoft.Json;
using FPT.Core.Extensions;
using FPT.Core.Equality;
using FPT.Core.Levels.Providers;
using FPT.Core.Levels;

namespace FPT.Core.Tests.Levels.Providers
{
    public class FileSystemLevelsProvider_GetLevelShould
    {
        [Theory]
        [ClassData(typeof(ReturnLevelOfSpecifiedId_Data))]
        public void ReturnLevelOfSpecifiedId(MockFileSystem fileSystem, string levelId, Level level)
        {
            var provider = new FileSystemLevelsProvider(fileSystem, "c:");

            var result = provider.GetLevel(levelId);

            Assert.Equal(level, result, new LevelEqualityComparer());
        }
        private class ReturnLevelOfSpecifiedId_Data : TheoryData<MockFileSystem, string, Level>
        {
            private const int levelCount = 4;
            private const int filesToAvoidCount = 10;
            private readonly Random random = new Random();
            private const int lengthOfRandomStrings = 10;
            public ReturnLevelOfSpecifiedId_Data()
            {
                var fileSystem = new MockFileSystem();
                var path = fileSystem.Path;
                for (int i = 0; i < levelCount; i++)
                {
                    var level = new Level(id: random.RandomString(lengthOfRandomStrings), folderFilepath: path.Combine("c:", path.GetRandomFileName()));
                    fileSystem.AddFile(path.Combine(level.FolderFilepath, "level.json"), JsonConvert.SerializeObject(level));
                    Add(fileSystem, level.Id, level);
                }
                for (int i = 0; i < filesToAvoidCount; i++)
                {
                    fileSystem.AddFile(path.Combine("c:", path.GetRandomFileName(), path.GetRandomFileName()), path.GetRandomFileName());
                }
            }
        }
        [Theory]
        [ClassData(typeof(ThrowWhenLevelNotFound_TestData))]
        public void ThrowWhenLevelNotFound(MockFileSystem fileSystem, string levelId, string expectedMessage)
        {
            var provider = new FileSystemLevelsProvider(fileSystem, "c:");

            var result = Assert.Throws<LevelNotFoundException>(() => provider.GetLevel(levelId));
            Assert.Equal(expectedMessage, result.Message);
        }
        public class ThrowWhenLevelNotFound_TestData : TheoryData<MockFileSystem, string, string>
        {
            private readonly Random random = new Random();
            private const int junkFileCount = 6;
            private const int junkLevelCount = 4;
            private const int testDataCount = 4;
            private const int randomStringLength = 10;
            public ThrowWhenLevelNotFound_TestData()
            {
                var fileSystem = new MockFileSystem(null, "c:");
                var path = fileSystem.Path;
                for (int i = 0; i < junkFileCount; i++)
                {
                    fileSystem.AddFile(path.Combine("c:", path.GetRandomFileName(), path.GetRandomFileName()), path.GetRandomFileName());
                }
                for (int i = 0; i < junkLevelCount; i++)
                {
                    var level = random.GenerateRandomLevel();
                    fileSystem.AddFile(path.Combine("c:", level.FolderFilepath, "level.json"), JsonConvert.SerializeObject(level));
                }
                for (int i = 0; i < testDataCount; i++)
                {
                    var levelId = random.RandomString(randomStringLength);
                    var errorMessage = $"No level with ID {levelId} was found.";
                    Add(fileSystem, levelId, errorMessage);
                }
            }
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("          ")]
        public void ThrowWhenNoLevelIdProvided(string levelId)
        {
            var fileSystem = new MockFileSystem();
            var expectedMessage = "No parameter for level ID was provided.";
            var provider = new FileSystemLevelsProvider(fileSystem, "c:");

            var result = Assert.Throws<InvalidCommandArrayException>(() => provider.GetLevel(levelId));
            Assert.Equal(expectedMessage, result.Message);
        }
    }
}
