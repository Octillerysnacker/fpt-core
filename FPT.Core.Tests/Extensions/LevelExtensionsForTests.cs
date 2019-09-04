using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Extensions;
using FPT.Core.Levels;
using FPT.Core.Tests.Levels.Providers;

namespace FPT.Core.Tests.Extensions
{
    public static class LevelExtensionsForTests
    {
        public static int LengthOfRandomStrings { get; set; } = 25;
        
        public static FakeLevelsProvider GenerateRandomLevelsWithProvider(this Random random, int minLevels, int maxLevels)
        {
            var amountOfLevels = random.Next(minLevels, maxLevels);
            var levels = new List<Level>();

            for (int i = 0; i < amountOfLevels; i++)
            {
                levels.Add(random.GenerateRandomLevel());
            }
            var levelsProvider = new FakeLevelsProvider(levels);
            return levelsProvider;
        }
        public static Level GenerateRandomLevel(this Random random)
        {
            string _() => random.RandomString(LengthOfRandomStrings);
            return new Level(_(), _(), _(), _(), _());
        }
    }
}
