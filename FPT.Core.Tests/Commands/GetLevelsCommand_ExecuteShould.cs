using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FPT.Core.Commands;
using FPT.Core.Levels;
using FPT.Core.Tests.Levels.Providers;

namespace FPT.Core.Tests.Commands
{
    public class GetLevelsCommand_ExecuteShould
    {
        [Fact]
        public void ReturnAllAvailableLevels()
        {
            var availableLevels = new Level[] { new Level(), new Level(), new Level() };
            var levelsProvider = new FakeLevelsProvider(availableLevels);
            var command = new GetLevelsCommand(levelsProvider);

            var result = (Level[])command.Execute();

            Assert.Equal(availableLevels, result);
        }
    }
}
