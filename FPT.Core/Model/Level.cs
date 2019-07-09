using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Model
{
    public class Level
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string InitializerFilepath { get; set; }
        public string VerifierFilepath { get; set; }
        public string InstructionsFilepath { get; set; }
    }
}
