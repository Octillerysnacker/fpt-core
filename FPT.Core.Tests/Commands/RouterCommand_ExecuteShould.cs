using FPT.Core.Commands;
using FPT.Core.Exceptions;
using System;
using Xunit;
using System.Linq;
using FPT.Core.Tests.Commands;

namespace FPT.Core.Tests
{
    public class RouterCommand_ExecuteShould{
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
        [ClassData(typeof(ThrowsArgumentExceptionWhenNoArgumentsSupplied_Data))]
        public void ThrowsnWhenNoArgumentsSupplied(string[] args)
        {
            var router = new RouterCommand();

            var e = Assert.Throws<InvalidCommandArrayException>(() => router.Execute(args));
            Assert.Equal("The given command array was invalid and could not be executed.", e.Message);
        }
        [Fact]
        public void ThrowsWhenCommandThrowsInvalidCommandArrayException()
        {
            var eToThrow = new InvalidCommandArrayException("The given command array was invalid and could no be executed.");
            var thrower = new ThrowerCommand(eToThrow);
            var router = new RouterCommand();
            router.Register("throw", thrower);

            var result = Assert.Throws<InvalidCommandArrayException>(() => router.Execute("throw"));
            Assert.Equal("A registered command was given an invalid command array.", result.Message);
            Assert.Equal(eToThrow, result.InnerException);
        }
        private class ThrowsArgumentExceptionWhenNoArgumentsSupplied_Data : TheoryData<string[]>
        {
            public ThrowsArgumentExceptionWhenNoArgumentsSupplied_Data()
            {
                Add(null);
                Add(new string[] { });
                Add(new string[] { null });
                Add(new[] { "" });
                Add(new[] { " " });
                Add(new[] { "           " });
            }
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
