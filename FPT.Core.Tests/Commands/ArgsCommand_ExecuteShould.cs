using FPT.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
namespace FPT.Core.Tests.Commands
{
    public class ArgsCommand_ExecuteShould
    {
        [Theory]
        [InlineData("the","quick","brown","fox")]
        [InlineData("jumps","over","the","lazy","dog")]
        public void SetArgsToExecuteParamters(params string[] args)
        {
            var ac = new ArgsCommand("");

            ac.Execute(args);

            Assert.True(args.SequenceEqual(ac.Args));
        }
    }
}
