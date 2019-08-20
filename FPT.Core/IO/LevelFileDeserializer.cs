using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using Newtonsoft.Json;
using System.Text;

namespace FPT.Core.IO
{
    public class LevelFileDeserializer
    {
        private readonly IFile file;
        private readonly IPath path;
        public LevelFileDeserializer(IFile file, IPath path)
        {
            this.file = file;
            this.path = path;
        }
        public Level DeserializeFromFile(string filepath)
        {
            string content = file.ReadAllText(filepath);

            var deserializedContent = JsonConvert.DeserializeObject<IDictionary<string, string>>(content);

            return new Level(
                name: deserializedContent["Name"],
                id: deserializedContent["Id"],
                initializerFilepath: deserializedContent["InitializerFilepath"],
                verifierFilepath: deserializedContent["VerifierFilepath"],
                instructionsFilepath: deserializedContent["InstructionsFilepath"],
                folderFilepath: path.GetDirectoryName(path.GetFullPath(filepath))
                );
        }
    }
}
