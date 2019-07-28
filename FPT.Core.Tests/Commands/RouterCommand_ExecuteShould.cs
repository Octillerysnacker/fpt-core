using FPT.Core.Commands;
using FPT.Core.Exceptions;
using System;
using Xunit;
using System.Linq;
using FPT.Core.Tests.Commands;

namespace FPT.Core.Tests
{
    public class RouterCommand_ExecuteShould{
        const string ArgumentExceptionErrorMessage = "Command ID must exist, not be null or empty.";
        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]

        public void ExecuteSpecifiedRegisteredCommand(string commandId){
            var ce = new RouterCommand();
            var commandToRun = new SignalCommand();
            var commandNotToRun = new SignalCommand();
            ce.Register(commandId, commandToRun);
            ce.Register("anotherCommand",commandNotToRun);

            ce.Execute(commandId);

            Assert.True(commandToRun.HasBeenRun);
            Assert.True(commandNotToRun.HasBeenRun == false);
        }
        [Fact]
        public void ThrowCommandNotFoundExceptionWhenNoCommandsRegistered()
        {
            var ce = new RouterCommand();
            const string UNREGISTERED_COMMAND_ID = "unregistered";

            Assert.Throws<CommandNotFoundException>(() => ce.Execute(UNREGISTERED_COMMAND_ID));
        }
        [Theory]
        [InlineData("test1")]
        [InlineData("test2")]
        public void ThrowCommandNotFoundExceptionWithMessage(string commandId)
        {
            var ce = new RouterCommand();

            var exception = Assert.Throws<CommandNotFoundException>(() => ce.Execute(commandId));

            Assert.Equal($"Command with ID {commandId} was not found.", exception.Message);
        }
        [Theory]
        [InlineData("test", "some", "args", "to", "pass")]
        [InlineData("test","many","more","args","to","pass","and","test")]
        public void PassAllParametersExceptCommandIdToCommandExecute(params string[] args)
        {
            var ce = new RouterCommand();
            var ac = new ArgsCommand();
            ce.Register("test", ac);

            ce.Execute(args);

            Assert.True(ac.Args.SequenceEqual(args.Skip(1)));
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void ThrowsArgumentExceptionWhenCommandIdIsNullOrEmptyWithMessage(string commandId)
        {
            var ce = new RouterCommand();

            var exception = Assert.Throws<ArgumentException>(() => ce.Execute(commandId));

            AssertThatArgumentExceptionMessageIsGood(exception);
        }
        [Fact]
        public void ThrowsArgumentExceptionWithMessageWhenArgsIsNull()
        {
            var ce = new RouterCommand();

            var exception = Assert.Throws<ArgumentException>(() => ce.Execute());

            AssertThatArgumentExceptionMessageIsGood(exception);
        }
        [Fact]
        public void ThrowsArgumentExceptionWithMessageWhenArgsIsEmpty()
        {
            var ce = new RouterCommand();

            var exception = Assert.Throws<ArgumentException>(() => ce.Execute(new string[0]));

            AssertThatArgumentExceptionMessageIsGood(exception);
        }
        private void AssertThatArgumentExceptionMessageIsGood(ArgumentException e)
        {
            Assert.Equal(ArgumentExceptionErrorMessage, e.Message);
        }
        [Theory]
        [InlineData(" test","test")]
        [InlineData("     test","test")]
        [InlineData("test      ","test")]
        [InlineData("test ","test")]
        [InlineData(" test ","test")]
        [InlineData("         test          ","test")]
        public void IgnoresLeadingAndTrailingWhitespaceInCommandIds(string commandId, string commandIdToExecute)
        {
            var ce = new RouterCommand();
            var command = new SignalCommand();
            ce.Register(commandId,command);

            ce.Execute(commandIdToExecute);

            Assert.True(command.HasBeenRun);
        }
        [Theory]
        [InlineData("return me")]
        [InlineData(3.14)]
        [InlineData(true)]
        public void ReturnResultOfExecutedCommand(object objToReturn)
        {
            string returnCommandId = "return";
            ReturnCommand returnCommand = new ReturnCommand(objToReturn);
            RouterCommand routerCommand = new RouterCommand();
            routerCommand.Register(returnCommandId, returnCommand);

            var result = routerCommand.Execute(returnCommandId);

            Assert.Equal(objToReturn, result);
        }
    }
}
