using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;
using FPT.Core.Commands;
using FPT.Core.IO;
using FPT.Core.Tests.IO;
using FPT.Core.Model;
using Xunit;
using FPT.Core.Tests.Extensions;
using FPT.Core.Extensions;
using System.IO;
using FPT.Core.Exceptions;

namespace FPT.Core.Tests.Commands
{
    public class OpenLevelCommand_ExecuteShould
    {
        [Theory]
        [ClassData(typeof(ProjectFolderTestData))]
        public void ReturnProjectFolderPathForSpecifiedLevelAndUser(FakeLevelsProvider provider, string levelId, string user, string expectedPath)
        {
            var command = new OpenLevelCommand(new MockLevelInitializer(), provider);

            var result = command.Execute(levelId, user);

            Assert.Equal(expectedPath, result);
        }
        public class ProjectFolderTestData : TheoryData<FakeLevelsProvider, string, string, string>
        {
            private readonly int dataSetCount = 4;
            private readonly RandomTriadFactory 
                factory = new RandomTriadFactory();
            public ProjectFolderTestData()
            {
                for (int i = 0; i < dataSetCount; i++)
                {
                    CreateAndAddRandomizedData();
                }
            }

            public int DataSetCount => DataSetCount1;

            public int DataSetCount1 => dataSetCount;

            private void CreateAndAddRandomizedData()
            {
                var triad = factory.CreateTriad();

                var expectedPath = Path.Combine(triad.Level.FolderFilepath, triad.User, "project");
                Add(triad.Provider, triad.Level.Id, triad.User, expectedPath);
            }
        }
        [Theory]
        [ClassData(typeof(CorrectParametersTestData))]
        public void SupplyCorrectParametersToInitializer(FakeLevelsProvider provider, string levelId, string user)
        {
            var initializer = new MockLevelInitializer();
            var command = new OpenLevelCommand(initializer, provider);

            command.Execute(levelId, user);

            Assert.Equal(levelId, initializer.SuppliedLevelId);
            Assert.Equal(user, initializer.SuppliedUser);
        }
        [Theory]
        [ClassData(typeof(TooFewParametersTestData))]
        public void ThrowWhenTooFewParametersPassed(string[] args, string errorMessage)
        {
            var command = new OpenLevelCommand(new MockLevelInitializer(), new FakeLevelsProvider(new List<Level>()));

            var result = Assert.Throws<InvalidCommandArrayException>(() => command.Execute(args));
            Assert.Equal(errorMessage, result.Message);
        }
        private class TooFewParametersTestData : TheoryData<string[], string>
        {
            private readonly Random random = new Random();
            private readonly int arbritraryLength = 25;
            public TooFewParametersTestData()
            {
                var errorMessage = "Not enough parameters were passed ({0} out of 2).";
                Add(null, string.Format(errorMessage, 0));
                Add(new string[] { }, string.Format(errorMessage, 0));
                Add(new string[] { random.RandomString(arbritraryLength) }, string.Format(errorMessage, 1));
            }
        }
        public class CorrectParametersTestData : TheoryData<FakeLevelsProvider,string, string>
        {
            private readonly int dataSetCount = 4;
            private readonly RandomTriadFactory factory = new RandomTriadFactory();
            public CorrectParametersTestData()
            {
                for(int i = 0; i< dataSetCount; i++)
                {
                    var triad = factory.CreateTriad();

                    Add(triad.Provider, triad.Level.Id, triad.User);
                }
            }
        }

    }
}
