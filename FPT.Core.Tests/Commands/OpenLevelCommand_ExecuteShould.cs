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
namespace FPT.Core.Tests.Commands
{
    public class OpenLevelCommand_ExecuteShould
    {
        [Theory]
        [ClassData(typeof(ProjectFolderTestData))]
        public void ReturnProjectFolderPathForSpecifiedLevelAndUser(FakeLevelsProvider provider, string levelId, string user, string expectedPath)
        {
            var command = new OpenLevelCommand("", new MockLevelInitializer(), provider);

            var result = command.Execute(levelId, user);

            Assert.Equal(expectedPath, result);
        }
        [Theory]
        [ClassData(typeof(CorrectParametersTestData))]
        public void SupplyCorrectParametersToInitializer(FakeLevelsProvider provider, string levelId, string user)
        {
            var initializer = new MockLevelInitializer();
            var command = new OpenLevelCommand("", initializer, provider);

            command.Execute(levelId, user);

            Assert.Equal(levelId, initializer.SuppliedLevelId);
            Assert.Equal(user, initializer.SuppliedUser);
        }
        public class ProjectFolderTestData : TheoryData<FakeLevelsProvider, string, string, string>
        {
            private int dataSetCount = 4;
            private RandomTriadFactory factory = new RandomTriadFactory();
            public ProjectFolderTestData()
            {
                for(int i = 0; i < dataSetCount; i++)
                {
                    CreateAndAddRandomizedData();
                }
            }
            private void CreateAndAddRandomizedData()
            {
                var triad = factory.CreateTriad();

                var expectedPath = Path.Combine(triad.Level.FolderFilepath, triad.User, "project");
                Add(triad.Provider, triad.Level.Id, triad.User, expectedPath);
            }
        }
        public class CorrectParametersTestData : TheoryData<FakeLevelsProvider,string, string>
        {
            private int dataSetCount = 4;
            private RandomTriadFactory factory = new RandomTriadFactory();
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
