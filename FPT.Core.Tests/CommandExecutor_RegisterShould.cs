using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Commands;
using Xunit;

namespace FPT.Core.Tests
{
    public class CommandExecutor_RegisterShould
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void RejectCommandsWithBlankCommandIds(string commandId)
        {
            var commandWithBlankId = new FakeCommand(commandId);
            var ce = new CommandExecutor();

            var e = Assert.Throws<ArgumentException>(() => ce.Register(commandWithBlankId));
            Assert.Equal("Command IDs cannot be blank.", e.Message);
        }
    }
}
