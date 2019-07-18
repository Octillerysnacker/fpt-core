using FPT.Core.Model;
using FPT.Core.Tests.IO;
using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Extensions;
namespace FPT.Core.Tests.Extensions
{
    public static class LevelExtensionsForTests
    {
        public static int LengthOfRandomStrings { get; set; } = 25;
        public static FakeLevelsProvider GenerateRandomLevelsWithProvider(this Random random, int minLevels, int maxLevels)
        {
            var amountOfLevels = random.Next(minLevels, maxLevels);
            var levels = new List<Level>();

            string _() => random.RandomString(LengthOfRandomStrings);

            for (int i = 0; i < amountOfLevels; i++)
            {
                levels.Add(new Level(_(),_(),_(),_(),_(),_()));
            }
            var levelsProvider = new FakeLevelsProvider(levels);
            return levelsProvider;
        }
    }
}
