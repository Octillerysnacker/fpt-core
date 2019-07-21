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
using FPT.Core.Tests.Extensions;
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
            var levelToInitialize = new Level(id: levelId, folderFilepath: ".");
            var levelsProvider = new FakeLevelsProvider(new[] { levelToInitialize });
            var mockFileSystem = new MockFileSystem();
            mockFileSystem.AddDirectory(Path.Combine(".", "master")); //will throw error if no master folder

            var masterFolderLevelInitializer = new MasterFolderLevelInitializer(initializationDeterminer,
                                                                                copyDir,
                                                                                levelsProvider,
                                                                                mockFileSystem.Path);

            var someUser = "user";
            masterFolderLevelInitializer.InitializeIfNecessary(levelId, someUser);

            Assert.Equal(shouldInitialize, copyDir.DidCopy);
            Assert.Equal(shouldInitialize, initializationDeterminer.IsMarkedAsInitialized);
        }
        [Theory]
        [ClassData(typeof(CopyCorrectFilesToCorrectLocation_TestData))]
        public void CopyCorrectFilesToCorrectLocation(FakeLevelsProvider fakeLevelsProvider, string levelId, string user, string expectedSourceDir, string expectedDestDir)
        {
            var determinerIndicatingInitialization = new MockLevelInitializationDeterminer(true);
            var copyDir = new MockCopyDir();
            var mockFileSystem = new MockFileSystem();
            foreach(var level in fakeLevelsProvider.GetLevels())
            {
                mockFileSystem.AddDirectory(mockFileSystem.Path.Combine(level.FolderFilepath, "master")); //put master folder in every level folder to prevent error
            }
            var masterFolderLevelInitializer = new MasterFolderLevelInitializer(determinerIndicatingInitialization,
                                                                                copyDir,
                                                                                fakeLevelsProvider,
                                                                                mockFileSystem.Path);

            masterFolderLevelInitializer.InitializeIfNecessary(levelId, user);

            Assert.Equal(expectedSourceDir, copyDir.SuppliedSourceDir);
            Assert.Equal(expectedDestDir, copyDir.SuppliedDestDir);
        }
        //TODO: Create separate class file for function
        [Theory]
        [ClassData(typeof(CopyCorrectFilesToCorrectLocation_TestData))]
        public void ThrowExceptionIfMasterFolderDoesNotExist(FakeLevelsProvider fakeLevelsProvider, string levelId, string user, string expectedSourceDir, string expectedDestDir)
        {
            var determinerIndicatingInitialization = new MockLevelInitializationDeterminer(true);
            var copyDir = new MockCopyDir();
            var path = new MockPath(new MockFileSystem());
            var masterFolderLevelInitializer = new MasterFolderLevelInitializer(determinerIndicatingInitialization,
                                                                                copyDir,
                                                                                fakeLevelsProvider,
                                                                                path);

            var exception = Assert.Throws<DirectoryNotFoundException>(() => masterFolderLevelInitializer.InitializeIfNecessary(levelId, user));
            Assert.Equal($"Master folder for level {levelId} not found.", exception.Message);
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

                var levelsProvider = random.GenerateRandomLevelsWithProvider(minLevelsPerProvider, maxLevelsPerProvider);
                var levelToInitialize = levelsProvider.GetLevels().RandomElementUsing(random);

                var userToInitialize = random.RandomString(arbritraryLength);

                var nameOfMasterFolder = "master";
                var nameOfProjectFolder = "project";
                var expectedSourceDir = Path.Combine(levelToInitialize.FolderFilepath, nameOfMasterFolder); //TO-DO: Remove logical dependencies on folder structure
                var expectedDestDir = Path.Combine(levelToInitialize.FolderFilepath, userToInitialize, nameOfProjectFolder);

                Add(levelsProvider, levelToInitialize.Id, userToInitialize, expectedSourceDir, expectedDestDir);
            }
        }
    }
}
