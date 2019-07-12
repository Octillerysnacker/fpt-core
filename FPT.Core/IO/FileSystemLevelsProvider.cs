using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Model;
using System.IO.Abstractions;
using System.Linq;
namespace FPT.Core.IO
{
    public class FileSystemLevelsProvider : ILevelsProvider
    {
        IFileSystem fileSystem;
        string rootFilepath;
        public FileSystemLevelsProvider(IFileSystem fileSystem, string rootFilepath)
        {
            this.fileSystem = fileSystem;
            this.rootFilepath = rootFilepath;
        }
        public IEnumerable<Level> GetLevels()
        {
            var levelFiles = fileSystem.Directory.EnumerateFiles(rootFilepath,"level.json",System.IO.SearchOption.AllDirectories);
            var deserializer = new LevelFileDeserializer(fileSystem.File, fileSystem.Path);
            return levelFiles.Select(levelFile => deserializer.DeserializeFromFile(levelFile));
        }
    }
}
