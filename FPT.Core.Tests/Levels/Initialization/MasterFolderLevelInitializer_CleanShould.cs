using FPT.Core.Levels.Initialization;
using FPT.Core.Tests.IO;
using FPT.Core.Tests.Levels.Providers;
using FPT.Core.Tests.Extensions;
using FPT.Core.Extensions;
using System.IO.Abstractions.TestingHelpers;
using Xunit;
using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.IO;
using FPT.Core.Levels;

namespace FPT.Core.Tests.Levels.Initialization
{
    public class MasterFolderLevelInitializer_CleanShould
    {
        [Theory]
        [ClassData(typeof(UnmarkUserFolder_Data))]
        public void UnmarkUserFolder(FakeLevelsProvider provider, string levelId, string user, string expectedUserFolder)
        {
            var determiner = new MockLevelInitializationDeterminer(false, true);
            var initializer = new MasterFolderLevelInitializer(determiner, new MockCopyDir(), provider , new MockPath(new MockFileSystem()));

            initializer.Clean(levelId, user);

            Assert.False(determiner.IsMarkedAsInitialized);
            Assert.Equal(expectedUserFolder, determiner.SuppliedUserFolderPath);
        }
        public class UnmarkUserFolder_Data : TheoryData<FakeLevelsProvider, string, string, string>
        {
            RandomTriadFactory factory = new RandomTriadFactory();
            private int datasetCount = 4;
            public UnmarkUserFolder_Data()
            {
                for(int i = 0; i < datasetCount; i++)
                {
                    var triad = factory.CreateTriad();
                    Add(triad.Provider, triad.Level.Id, triad.User, triad.Level.GetUserFolder(triad.User));
                }
            }
        }
        [Theory]
        [ClassData(typeof(RemoveContentsOfProjectFolder_Data))]
        public void RemoveContentsOfProjectFolder(MockFileSystem fs,FakeLevelsProvider provider, Level level, string user)
        {
            var initializer = new MasterFolderLevelInitializer(new MockLevelInitializationDeterminer(false), new MockCopyDir(), provider, fs.Path);

            initializer.Clean(level.Id, user);

            Assert.Empty(fs.Directory.EnumerateFileSystemEntries("C:\\"+level.GetProjectFolder(user)));
        }
        public class RemoveContentsOfProjectFolder_Data : TheoryData<MockFileSystem, FakeLevelsProvider, Level, string>
        {
            private readonly Random random = new Random();
            private readonly int datasetCount = 4;
            private readonly int minContentLength = 0;
            private readonly int maxContentLength = 20;
            private readonly int minFiles = 1;
            private readonly int maxFiles = 10;
            private readonly int minUserLength = 1;
            private readonly int maxUserLength = 10;
            public RemoveContentsOfProjectFolder_Data()
            {
                for(int i = 0; i < datasetCount; i++)
                {
                    var level = random.GenerateRandomLevel();
                    var provider = new FakeLevelsProvider(new[] { level });
                    var user = random.NextString(minUserLength, maxUserLength);
                    var fs = new MockFileSystem();
                    var projectDir = fs.Directory.CreateDirectory(level.GetProjectFolder(user));
                    var fileCount = random.Next(minFiles, maxFiles);
                    for(int j = 0; j < fileCount; j++)
                    {
                        fs.AddFile(fs.Path.Combine(projectDir.FullName,fs.Path.GetRandomFileName()), random.NextMockFileData(minContentLength, maxContentLength));
                    }
                    Add(fs, provider, level, user);
                }
            }
        }
    }
}
