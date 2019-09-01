using FPT.Core.Extensions;
using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xunit;
namespace FPT.Core.Tests.IO
{
    public class JarFileProcessFactory_CreateProcessShould
    {
        [Fact]
        public void CorrectlyRunJarFile()
        {
            var factory = new JarFileProcessFactory();

            var process = factory.CreateProcess(@"./IO/JarFileProcessFactory_TestJars/testPassed.jar", "", "");
            var output = process.StandardOutput.ReadToEnd();

            Assert.Equal("Test Passed!\r\n", output);
            Assert.Equal("java", (process as Process).StartInfo.FileName);
        }
        [Theory]
        [ClassData(typeof(PassCorrectParametersToJar_Data))]
        public void PassCorrectParametersToJar(string userFolder, string projectFolder)
        {
            var factory = new JarFileProcessFactory();

            var process = factory.CreateProcess(@"./IO/JarFileProcessFactory_TestJars/outputParams.jar", userFolder, projectFolder);

            Assert.Equal($"{userFolder}", process.StandardOutput.ReadLine());
            Assert.Equal($"{projectFolder}", process.StandardOutput.ReadLine());
        }
        private class PassCorrectParametersToJar_Data : TheoryData<string, string>
        {
            private int datasetCount = 4;
            private Random random = new Random();
            public PassCorrectParametersToJar_Data()
            {
                for(int i = 0; i < datasetCount; i++)
                {
                    Add(random.RandomString(15), random.RandomString(15));
                }
            }
        }
    }
}
