using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FPT.Core.Model;
using FPT.Core.Commands;
using FPT.Core.Tests.IO;

namespace FPT.Core.Tests.Commands
{
    public class GetLevelsCommand_ExecuteShould
    {
        [Fact]
        public void ReturnAllAvailableLevels()
        {
            var context = new FakeFPTContext();
            var availableLevels = new Level[] { new Level(), new Level(), new Level() };
            context.Levels = availableLevels;
            var command = new GetLevelsCommand("",context);

            var result = (Level[])command.Execute();

            Assert.Equal(availableLevels, result);
        }
    }
}
