using FPT.Core.Commands;
using FPT.Core.Tests.IO;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using Xunit;
using FPT.Core.Model;
using FPT.Core.Exceptions;

namespace FPT.Core.Tests.Commands
{
    public class GetInstructionsPathCommand_ExecuteShould
    {
        [Theory]
        [ClassData(typeof(ReturnInstructionsPathForSpecifiedLevel_Data))]
        public void ReturnInstructionsPathForSpecifiedLevel(FakeLevelsProvider provider, string levelId, string expectedPath)
        {
            var command = new GetInstructionsPathCommand(provider);

            var result = command.Execute(levelId);

            Assert.Equal(expectedPath, result);
        }
        private class ReturnInstructionsPathForSpecifiedLevel_Data : TheoryData<FakeLevelsProvider, string, string>
        {
            private int dataSetCount = 4;
            private RandomTriadFactory factory = new RandomTriadFactory();
            public ReturnInstructionsPathForSpecifiedLevel_Data()
            {
                for(int i = 0; i < dataSetCount; i++)
                {
                    var triad = factory.CreateTriad();
                    var expectedPath = Path.Combine(triad.Level.FolderFilepath, triad.Level.InstructionsFilepath);
                    Add(triad.Provider, triad.Level.Id, expectedPath);
                }
            }
        }
        [Theory]
        [InlineData(null)]
        [ClassData(typeof(ThrowWhenTooFewParametersPassed_Data))]
        public void ThrowWhenTooFewParametersPassed(string[] args)
        {
            var command = new GetInstructionsPathCommand(new FakeLevelsProvider(new Level[] { }));

            var e = Assert.Throws<InvalidCommandArrayException>(() => command.Execute(args));
            Assert.Equal("Not enough parameters were passed (0 out of 1).", e.Message);
        }
        private class ThrowWhenTooFewParametersPassed_Data : TheoryData<string[]>
        {
            public ThrowWhenTooFewParametersPassed_Data()
            {
                Add(new string[] { });
            }
        }
    }
}
