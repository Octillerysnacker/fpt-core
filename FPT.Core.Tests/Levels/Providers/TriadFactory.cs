using FPT.Core.Extensions;
using FPT.Core.Tests.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.Levels.Providers
{
    public class RandomTriadFactory
    {
        public Random Random { get; set; } = new Random();
        public int MinLevels { get; set; } = 1;
        public int MaxLevels { get; set; } = 10;
        public int ArbritraryLength { get; set; } = 25;
        public Triad CreateTriad(Random random, int minLevels, int maxLevels, int arbritraryLength)
        {

            var provider = random.GenerateRandomLevelsWithProvider(minLevels, maxLevels);
            var level = provider.GetLevels().RandomElementUsing(random);
            var user = random.RandomString(arbritraryLength);

            return new Triad(provider, level, user);
        }
        public Triad CreateTriad()
        {
            return CreateTriad(Random, MinLevels, MaxLevels, ArbritraryLength);
        }
    }
}
