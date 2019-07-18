using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using Xunit;
using System.Linq;
using FPT.Core.IO;
using FPT.Core.Model;
using FPT.Core.Extensions;
namespace FPT.Core.Tests.IO
{
    public class MasterFolderLevelInitializer_InitializeIfNecessaryShould
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void CopyFilesWhenIndicated(bool shouldInitialize)
        {
            var initializationDeterminer = new MockLevelInitializationDeterminer(shouldInitialize);

            var copyDir = new MockCopyDir();

            var levelId = "level";
            var levelToInitialize = new Level(id: levelId);
            var levelsProvider = new FakeLevelsProvider(new[] { levelToInitialize });

            var path = new MockPath(new MockFileSystem());

            var masterFolderLevelInitializer = new MasterFolderLevelInitializer(initializationDeterminer,
                                                                                copyDir,
                                                                                levelsProvider,
                                                                                path);

            var someUser = "user";
            masterFolderLevelInitializer.InitializeIfNecessary(levelId, someUser);

            Assert.Equal(shouldInitialize, copyDir.DidCopy);
        }
        [Theory]
        [ClassData(typeof(CopyCorrectFilesToCorrectLocation_TestData))]
        public void CopyCorrectFilesToCorrectLocation(FakeLevelsProvider fakeLevelsProvider, string levelId, string user, string expectedSourceDir, string expectedDestDir)
        {
            var determinerIndicatingInitialization = new MockLevelInitializationDeterminer(true);
            var copyDir = new MockCopyDir();
            var path = new MockPath(new MockFileSystem());
            var masterFolderLevelInitializer = new MasterFolderLevelInitializer(determinerIndicatingInitialization,
                                                                                copyDir,
                                                                                fakeLevelsProvider,
                                                                                path);

            masterFolderLevelInitializer.InitializeIfNecessary(levelId, user);

            Assert.Equal(expectedSourceDir, copyDir.SuppliedSourceDir);
            Assert.Equal(expectedDestDir, copyDir.SuppliedDestDir);
        }
        public class CopyCorrectFilesToCorrectLocation_TestData : TheoryData<FakeLevelsProvider, string, string ,string, string>
        {
            private Random random = new Random();
            private int minLevelsPerProvider = 1;
            private int maxLevelsPerProvider = 10;
            private int arbritraryLength = 25;
            private int numberOfDataSets = 4;
            public CopyCorrectFilesToCorrectLocation_TestData()
            {
                for(int i = 0; i < numberOfDataSets; i++)
                {
                    CreateAndAddRandomizedData();
                }
            }
            private void CreateAndAddRandomizedData()
            {
                var amountOfLevels = random.Next(minLevelsPerProvider, maxLevelsPerProvider);
                var levels = new List<Level>();
                for(int i = 0; i < amountOfLevels; i++)
                {
                    levels.Add(GenerateRandomLevelWithNecessaryProperties());
                }

                var levelToInitialize = GenerateRandomLevelWithNecessaryProperties();
                levels.Add(levelToInitialize);

                var userToInitialize = random.RandomString(arbritraryLength);
                var levelsProvider = new FakeLevelsProvider(levels);

                var nameOfMasterFolder = "master";
                var nameOfProjectFolder = "project";
                var expectedSourceDir = Path.Combine(levelToInitialize.FolderFilepath, nameOfMasterFolder); //TO-DO: Remove logical dependencies on folder structure
                var expectedDestDir = Path.Combine(levelToInitialize.FolderFilepath, userToInitialize, nameOfProjectFolder);

                Add(levelsProvider, levelToInitialize.Id, userToInitialize, expectedSourceDir, expectedDestDir);
            }
            private Level GenerateRandomLevelWithNecessaryProperties()
            {
                return new Level(id: random.RandomString(arbritraryLength),
                                 folderFilepath: Path.Combine("c:",Path.GetRandomFileName()));
            }
        }
    }
}
