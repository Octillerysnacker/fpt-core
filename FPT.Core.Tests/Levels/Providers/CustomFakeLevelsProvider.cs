using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Levels;
using FPT.Core.Levels.Providers;

namespace FPT.Core.Tests.Levels.Providers
{
    public class CustomFakeLevelsProvider : ILevelsProvider
    {
        public Level Level { get; set; }
        public Level GetLevel(string levelId)
        {
            return Level;
        }

        public IEnumerable<Level> GetLevels()
        {
            return new[] { Level };
        }
    }
}
