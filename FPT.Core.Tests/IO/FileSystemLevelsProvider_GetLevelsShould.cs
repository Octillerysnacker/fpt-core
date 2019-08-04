using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.IO.Abstractions.TestingHelpers;
using System.IO.Abstractions;
using System.Collections;
using FPT.Core.Model;
using FPT.Core.IO;
using Newtonsoft.Json;

namespace FPT.Core.Tests.IO
{
    public class FileSystemLevelsProvider_GetLevelsShould
    {
        [Theory]
        [ClassData(typeof(FindAndReturnAllLevelsInFolder_TestData))]
        public void FindAndReturnAllLevelsInFolder(MockFileSystem mockFileSystem, string filepath, IEnumerable<Level> levelsToFind)
        {
            FileSystemLevelsProvider fileSystemLevelsProvider = new FileSystemLevelsProvider(mockFileSystem,filepath);

            var result = fileSystemLevelsProvider.GetLevels();

            Assert.Equal(levelsToFind, result, new LevelEqualityComparer());
        }
        public class FindAndReturnAllLevelsInFolder_TestData : TheoryData<MockFileSystem, string, IEnumerable<Level>>
        {
            readonly MockFileSystem mockFileSystem = new MockFileSystem();
            readonly FolderPathWithLevels set1 = new FolderPathWithLevels() { FolderPath = "c:\\set1" };
            readonly FolderPathWithLevels set2 = new FolderPathWithLevels() { FolderPath = "c:\\generic folder\\set2" };
            readonly FolderPathWithLevels set3 = new FolderPathWithLevels() { FolderPath = "c:\\set3" };
            readonly FolderPathWithLevels set4 = new FolderPathWithLevels() { FolderPath = "c:\\Documents\\set4" };
            public FindAndReturnAllLevelsInFolder_TestData()
            {
                AddFileToBeAvoidedToSet(set1, "\\.gitignore", "**");
                AddFileToBeAvoidedToSet(set1, "\\downloads\\datboi.jpg", "here come dat boi");
                AddFileToBeAvoidedToSet(set2, "\\initializer.bat", "echo No stop");
                AddFileToBeAvoidedToSet(set2, "\\doggo.exe", "This is a virus");
                AddFileToBeAvoidedToSet(set3, "\\rocketleague.exe", "The best game");
                AddFileToBeAvoidedToSet(set4, "\\deeply\\nested\\passwords.json", "Password: qwerty1234");

                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "Bruh",
                    folderFilepath: set1.FolderPath + "\\bruh"), set1);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "Lmfao",
                    folderFilepath: set1.FolderPath + "\\negligent"), set1);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "Number 3",
                    folderFilepath: set1.FolderPath + "\\nested\\folders\\yea"), set1);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "I'm",
                    folderFilepath: set2.FolderPath + "\\soeifoseif"), set2);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "getting",
                    folderFilepath: set2.FolderPath + "\\asdasdasd"), set2);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "tired",
                    folderFilepath: set3.FolderPath + "\\fwafeegsgesseg"), set3);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "of",
                    folderFilepath: set3.FolderPath + "\\hernia"), set3);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "data",
                    folderFilepath: set4.FolderPath + "\\discs"), set4);
                AddLevelToFileSystemAndFolderPathSet(new Level(
                    name: "entry",
                    folderFilepath: set4.FolderPath + "\\finally"), set4);

                AddTestDataWrap(set1);
                AddTestDataWrap(set2);
                AddTestDataWrap(set3);
                AddTestDataWrap(set4);
            }
            private void AddLevelToFileSystemAndFolderPathSet(Level level, FolderPathWithLevels folderPathWithLevels)
            {
                folderPathWithLevels.Levels.Add(level);
                CreateAndAddDataFromLevelToMockFileSystem(level);
            }
            private void CreateAndAddDataFromLevelToMockFileSystem(Level level)
            {
                string mockFilePath = level.FolderFilepath + "\\level.json";
                var mockFileData = new MockFileData(JsonConvert.SerializeObject(level));

                mockFileSystem.AddFile(mockFilePath, mockFileData);
            }
            private void AddTestDataWrap(FolderPathWithLevels folderPathWithLevels)
            {
                Add(mockFileSystem, folderPathWithLevels.FolderPath, folderPathWithLevels.Levels);
            }
            private class FolderPathWithLevels
            {
                public string FolderPath { get; set; }
                public List<Level> Levels { get; set; } = new List<Level>();
            }
            private void AddFileToBeAvoidedToSet(FolderPathWithLevels folderPathWithLevels, string filepath, string content)
            {
                mockFileSystem.AddFile(folderPathWithLevels.FolderPath + filepath, content);
            }
        }
    }
}
