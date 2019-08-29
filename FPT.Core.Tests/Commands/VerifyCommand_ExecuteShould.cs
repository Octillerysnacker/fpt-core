using FPT.Core.Commands;
using FPT.Core.Exceptions;
using FPT.Core.Extensions;
using FPT.Core.Model;
using FPT.Core.Tests.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FPT.Core.Tests.Commands
{
    public class VerifyCommand_ExecuteShould
    {
        [Theory]
        [ClassData(typeof(RunCorrectVerifierForUser_Data))]
        public void RunCorrectVerifierForUser(FakeLevelsProvider provider, string levelId, string user)
        {
            var mockVerifier = new MockVerifier();
            var command = new VerifyCommand(provider,mockVerifier);

            command.Execute(levelId, user);

            Assert.Equal(levelId, mockVerifier.Level.Id);
            Assert.Equal(user, mockVerifier.User);
        }
        private class RunCorrectVerifierForUser_Data : TheoryData<FakeLevelsProvider, string, string>
        {
            private int datasetCount = 4;
            private RandomTriadFactory factory = new RandomTriadFactory();
            public RunCorrectVerifierForUser_Data()
            {
                for(int i = 0; i < datasetCount; i++)
                {
                    var triad = factory.CreateTriad();
                    Add(triad.Provider, triad.Level.Id, triad.User);
                }
            }
        }
        [Theory]
        [ClassData(typeof(ReturnResultFromVerifier_Data))]
        public void ReturnResultFromVerifier(VerifierResult toReturn)
        {
            var verifier = new MockVerifier() { Return = toReturn };
            var provider = new CustomFakeLevelsProvider() { Level = null };
            var command = new VerifyCommand(provider, verifier);

            var result = command.Execute("", "");

            Assert.Equal(toReturn, result);
        }
        private class ReturnResultFromVerifier_Data : TheoryData<VerifierResult>
        {
            public ReturnResultFromVerifier_Data()
            {
                Add(null);
                Add(new VerifierResult(false, null));
            }
        }
        [Theory]
        [ClassData(typeof(ThrowWhenTooFewParametersPassed_Data))]
        public void ThrowWhenTooFewParametersPassed(string[] args, string expected)
        {
            var verifier = new MockVerifier();
            var provider = new CustomFakeLevelsProvider();
            var command = new VerifyCommand(provider, verifier);

            var e = Assert.Throws<InvalidCommandArrayException>(() => command.Execute(args));
            Assert.Equal(expected, e.Message);
        }
        private class ThrowWhenTooFewParametersPassed_Data : TheoryData<string[], string>
        {
            private Random random = new Random();
            public ThrowWhenTooFewParametersPassed_Data()
            {
                Add(null, FormatMessage(0));
                Add(new string[] { }, FormatMessage(0));
                Add(new string[] { random.RandomString(10) }, FormatMessage(1));
            }
            private string FormatMessage(int missingParamCount)
            {
                return $"Not enough parameters were passed ({missingParamCount} out of 2).";
            }
        }
    }
}
