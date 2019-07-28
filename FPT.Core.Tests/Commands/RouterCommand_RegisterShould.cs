using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Commands;
using Xunit;

namespace FPT.Core.Tests
{
    public class RouterCommand_RegisterShould
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        public void RejectCommandsWithBlankCommandIds(string commandId)
        {
            var commandWithBlankId = new FakeCommand();
            var ce = new RouterCommand();

            var e = Assert.Throws<ArgumentException>(() => ce.Register(commandId, commandWithBlankId));
            Assert.Equal("Command IDs cannot be blank.", e.Message);
        }
    }
}
