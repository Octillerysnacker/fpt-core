using FPT.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FPT.Core.Tests.Commands
{
    public class SignalCommandTest
    {
        [Fact]
        public void ExecuteShouldSetHasBeenRunToTrue()
        {
            var sc = new SignalCommand("");
            sc.Execute();
            Assert.True(sc.HasBeenRun);
        }
        [Fact]
        public void HasBeenRunShouldBeFalseBeforeExecuting()
        {
            var sc = new SignalCommand("");
            Assert.False(sc.HasBeenRun);
        }
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void HasBeenRunShouldBeTrueAfterMultipleExecutions(int executions)
        {
            var sc = new SignalCommand("");
            for(int i = 0; i < executions; i++)
            {
                sc.Execute();
            }
            Assert.True(sc.HasBeenRun);
        }
    }
}
