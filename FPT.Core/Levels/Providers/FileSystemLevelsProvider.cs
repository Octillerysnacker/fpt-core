using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Exceptions;
using System.IO.Abstractions;
using System.Linq;

namespace FPT.Core.Levels.Providers
{
    public class FileSystemLevelsProvider : ILevelsProvider
    {
        readonly IFileSystem fileSystem;
        readonly string rootFilepath;
        public FileSystemLevelsProvider(IFileSystem fileSystem, string rootFilepath)
        {
            this.fileSystem = fileSystem;
            this.rootFilepath = rootFilepath;
        }

        public Level GetLevel(string levelId)
        {
            if (string.IsNullOrWhiteSpace(levelId))
            {
                throw new InvalidCommandArrayException("No parameter for level ID was provided.");
            }
            try
            {
                return GetLevels().Single(level => level.Id == levelId);
            }
            catch (InvalidOperationException)
            {
                throw new LevelNotFoundException($"No level with ID {levelId} was found.");
            }
        }

        public IEnumerable<Level> GetLevels()
        {
            var levelFiles = fileSystem.Directory.EnumerateFiles(rootFilepath, "level.json", System.IO.SearchOption.AllDirectories);
            var deserializer = new LevelFileDeserializer(fileSystem.File, fileSystem.Path);
            return levelFiles.Select(levelFile => deserializer.DeserializeFromFile(levelFile));
        }
    }
}
