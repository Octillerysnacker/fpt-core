using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FPT.Core.Tests.IO
{
    public class RandomTriadFactory_CreateTriadShould
    {
        [Fact]
        public void ReturnATriadWithProviderContainingLevel()
        {
            var factory = new RandomTriadFactory();

            var triad = factory.CreateTriad();

            Assert.Contains(triad.Provider.Levels, l => new LevelEqualityComparer().Equals(l, triad.Level));
        }
    }
}
