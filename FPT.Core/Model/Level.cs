using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Model
{
    public class Level
    {
        public string Name { get; }
        public string Id { get; }
        public string InitializerFilepath { get; }
        public string VerifierFilepath { get; }
        public string InstructionsFilepath { get; }
        public Level(string name, string id, string initializerFilepath, string verifierFilepath, string instructionsFilepath)
        {
            Name = name;
            Id = id;
            InitializerFilepath = initializerFilepath;
            VerifierFilepath = verifierFilepath;
            InstructionsFilepath = instructionsFilepath;
        }
    }
}
