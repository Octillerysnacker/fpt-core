using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using FPT.Core.Levels.Providers;
using FPT.Core.Levels;

namespace FPT.Core.Tests.Levels.Providers
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
