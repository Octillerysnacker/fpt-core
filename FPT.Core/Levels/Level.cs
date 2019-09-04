using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Levels
{
    public class Level
    {
        public string Name { get; }
        public string Id { get; }
        public string VerifierFilepath { get; }
        public string InstructionsFilepath { get; }
        public string FolderFilepath { get; }
        public Level(string name = "", string id = "", string verifierFilepath = "", string instructionsFilepath = "", string folderFilepath = "")
        {
            Name = name;
            Id = id;
            VerifierFilepath = verifierFilepath;
            InstructionsFilepath = instructionsFilepath;
            FolderFilepath = folderFilepath;
        }
    }
}
