using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Abstractions.TestingHelpers;
using FPT.Core.IO;
using System.IO;
using Xunit;
using System.Linq;
namespace FPT.Core.Tests.IO
{
    public class CopyDir_CopyAllShould
    {
        [Theory]
        [ClassData(typeof(CopyAllFilesAndSubdirectories_TestData))]
        public void CopyAllFilesAndSubdirectories(MockFileSystem mockFileSystem, string sourceDir, string destinationDir, MockFileSystem expectedMockFileSystem)
        {
            var copyDir = new CopyDir(mockFileSystem.Directory, mockFileSystem.Path);
            var mockFactory = new MockDirectoryInfoFactory(mockFileSystem);

            copyDir.CopyAll(mockFactory.FromDirectoryName(sourceDir), mockFactory.FromDirectoryName(destinationDir));

            //TO-DO: Replace with proper equality comparer
            Assert.Equal(mockFileSystem.AllPaths.OrderBy(s => s), expectedMockFileSystem.AllPaths.OrderBy(s => s));
            Assert.Equal(mockFileSystem.AllFiles.Select(filePath => mockFileSystem.GetFile(filePath).TextContents).OrderBy(s => s), expectedMockFileSystem.AllFiles.Select(filePath => expectedMockFileSystem.GetFile(filePath).TextContents).OrderBy(s => s));
        }
        private class CopyAllFilesAndSubdirectories_TestData : TheoryData<MockFileSystem, string, string, MockFileSystem>
        {
            private static Random random = new Random();
            public CopyAllFilesAndSubdirectories_TestData()
            {
                var dataCount = 4;
                for(int i = 0; i < dataCount; i++)
                {
                    AddData();
                }
            }
            private static string GenerateRandomAlphanumericString(int length)
            {
                string validChars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890";
                StringBuilder stringBuilder = new StringBuilder();
                for(int i = 0; i < length; i++)
                {
                    stringBuilder.Append(validChars[random.Next(validChars.Length)]);
                }
                return stringBuilder.ToString();
            }
            private void AddData()
            {
                var stemFiles = new List<StemFile>();
                for(int i = 0; i < 10; i++)
                {
                    stemFiles.Add(StemFile.GenerateRandomStemFile());
                }

                var startingMockFileSystem = new MockFileSystem();
                var finalMockFileSytem = new MockFileSystem();
                var sourceDir = Path.Combine("c:", Path.GetRandomFileName());
                var destDir = Path.Combine("c:", Path.GetRandomFileName());
                foreach(var stemFile in stemFiles)
                {
                    startingMockFileSystem.AddFile(Path.Combine(sourceDir, stemFile.FilePathStem), stemFile.MockFileData);
                    finalMockFileSytem.AddFile(Path.Combine(sourceDir, stemFile.FilePathStem), stemFile.MockFileData);

                    finalMockFileSytem.AddFile(Path.Combine(destDir, stemFile.FilePathStem), stemFile.MockFileData);
                }

                Add(startingMockFileSystem, sourceDir, destDir, finalMockFileSytem);
            }
            private class StemFile
            {
                public string FilePathStem { get; set; }
                public MockFileData MockFileData { get; set; }
                public static StemFile GenerateRandomStemFile()
                {
                    return new StemFile()
                    {
                        FilePathStem = Path.Combine(Path.GetRandomFileName(), Path.GetRandomFileName()),
                        MockFileData = new MockFileData(GenerateRandomAlphanumericString(50))
                    };
                }
            }
        }
    }
}
