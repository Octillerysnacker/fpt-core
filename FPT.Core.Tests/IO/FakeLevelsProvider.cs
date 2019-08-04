using FPT.Core.IO;
using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace FPT.Core.Tests.IO
{
    public class FakeLevelsProvider : ILevelsProvider
    {
        public IEnumerable<Level> Levels { get; }
        public FakeLevelsProvider(IEnumerable<Level> levels)
        {
            Levels = levels;
        }

        public IEnumerable<Level> GetLevels()
        {
            return Levels;
        }

        public Level GetLevel(string levelId)
        {
            return Levels.Single(level => level.Id == levelId);
        }
    }
}
