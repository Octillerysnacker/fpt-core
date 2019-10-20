using FPT.Core.Commands;
using FPT.Core.Tests.Levels.Initialization;
using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Extensions;
using Xunit;
using FPT.Core.Exceptions;

namespace FPT.Core.Tests.Commands
{
    public class ResetLevelCommand_ExecuteShould
    {
        [Theory]
        [ClassData(typeof(SendCorrectParametersToInitializer_Data))]
        public void SendCorrectParametersToInitialzer(string levelId, string user)
        {
            var initializer = new MockLevelInitializer();
            var command = new ResetLevelCommand(initializer);

            command.Execute(levelId, user);

            Assert.Equal(levelId, initializer.CleanedLevelId);
            Assert.Equal(user, initializer.CleanedUser);
            Assert.Equal(levelId, initializer.InitializedLevelId);
            Assert.Equal(user, initializer.InitializedUser);
        }
        private class SendCorrectParametersToInitializer_Data : TheoryData<string, string>
        {
            private readonly Random random = new Random();
            private readonly int datasetCount = 4;
            private readonly int minStringLength = 1;
            private readonly int maxStringLength = 25;

            public SendCorrectParametersToInitializer_Data()
            {
                for(int i = 0; i< datasetCount; i++)
                {
                    Add(NextString(), NextString());
                }
            }
            private string NextString()
            {
                return random.NextString(minStringLength, maxStringLength);
            }
        }
        [Theory]
        [ClassData(typeof(ThrowIfInvalidParametersPassed_Data))]
        
        public void ThrowIfInvalidParametersPassed(string[] args)
        {
            var command = new ResetLevelCommand(new MockLevelInitializer());

            Assert.Throws<InvalidCommandArrayException>(() => command.Execute(args));
        }
        public class ThrowIfInvalidParametersPassed_Data : TheoryData<string[]>
        {
            public ThrowIfInvalidParametersPassed_Data()
            {
                Add(null);
                Add(new string[0]);
                Add(new string[] { null, null });
                Add(new[] { "" });
                Add(new[] { "      " });
                Add(new[] { null, "    " });
                Add(new[] { "    ", null });
                Add(new[] { "        ", "        " });
            }
        }
    }
}
