﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Abstractions.TestingHelpers;
using FPT.Core.Model;
using FPT.Core.IO;
using Xunit;
using Newtonsoft.Json;
namespace FPT.Core.Tests.IO
{
    public class LevelFileDeserializer_DeserializeFileShould
    {
        [Theory]
        [ClassData(typeof(DeserializeLevelJsonFileIntoLevel_TestData))]
        public void DeserializeLevelJsonFileIntoLevel(MockFileSystem mockFileSystem, string filepath, Level expected)
        {
            LevelFileDeserializer deserializer = new LevelFileDeserializer(new MockFile(mockFileSystem), new MockPath(mockFileSystem));

            Level level = deserializer.DeserializeFromFile(filepath);

            Assert.Equal(expected, level,new LevelEqualityComparer());
        }
        public class DeserializeLevelJsonFileIntoLevel_TestData : TheoryData<MockFileSystem, string, Level>
        {
            public DeserializeLevelJsonFileIntoLevel_TestData()
            {
                CreateAndAddDataFromLevel(new Level(
                    name: "Test Level 1",
                    id: "test.1",
                    initializerFilepath: "c:\\levels\\test1\\initializer.bat",
                    verifierFilepath: "c:\\levels\\test1\\verifier.bat",
                    instructionsFilepath: "c:\\levels\\test1\\instructions.md",
                    folderFilepath: "c:\\levels\\test1"
                    ));
                CreateAndAddDataFromLevel(new Level(
                    name: "FRC Programming Trainer",
                    id: "fpt.main",
                    initializerFilepath: "c:\\fpt\\init.sh",
                    verifierFilepath: "c:\\fpt\\verifier.sh",
                    instructionsFilepath: "c:\\fpt\\instructions.md",
                    folderFilepath: "c:\\fpt"
                    ));
                CreateAndAddDataFromLevel(new Level(
                    name: "Java: Variables Introduction",
                    id: "java.basics.variables",
                    initializerFilepath: "c:\\levels\\java\\variables\\initializer.js",
                    verifierFilepath: "c:\\levels\\java\\variables\\verifier.js",
                    instructionsFilepath: "c:\\levels\\java\\variables\\instructions.md",
                    folderFilepath: "c:\\levels\\java\\variables"
                    ));
            }
            private void CreateAndAddDataFromLevel(Level level)
            {
                string mockFilePath = level.FolderFilepath + "\\level.json";

                var mockFileData = new MockFileData(JsonConvert.SerializeObject(level));
                var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>() {
                    {mockFilePath,mockFileData }
                });

                Add(mockFileSystem, mockFilePath, level);
            }
        }
    }
}
