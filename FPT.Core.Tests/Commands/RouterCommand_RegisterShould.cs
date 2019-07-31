using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Commands;
using Xunit;
using FPT.Core.Extensions;
using FPT.Core.Tests.Extensions;
namespace FPT.Core.Tests
{
    public class RouterCommand_RegisterShould
    {
        [Theory]
        [InlineData(null)]
        [ClassData(typeof(Data))]
        public void RejectNonAlphanumericCommandIds(string commandId)
        {
            var command = new FakeCommand();
            var router = new RouterCommand();

            var e = Assert.Throws<ArgumentException>(() => router.Register(commandId, command));
            Assert.Equal("Command ID must be alphanumeric.", e.Message);
        }
        private class Data : TheoryData<string>
        {
            private string nonAlphanumericChars = "~!@#$%^&*()`-_+=[]{}|\"\\';:?/>.<, ";
            private Random random = new Random();
            private int dataCount = 4;
            private int stringLengths = 25;
            public Data()
            {
                for(int i = 0; i < dataCount; i++)
                {
                    var sb = new StringBuilder();
                    for(int j = 0; j < stringLengths; j++)
                    {
                        sb.Append(nonAlphanumericChars.RandomElementUsing(random));
                    }
                    Add(sb.ToString());
                }
            }
        }
    }
}
