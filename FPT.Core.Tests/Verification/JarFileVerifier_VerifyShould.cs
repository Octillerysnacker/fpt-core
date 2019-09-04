using FPT.Core.Equality;
using FPT.Core.Extensions;
using FPT.Core.Levels;
using FPT.Core.Tests.Extensions;
using FPT.Core.Tests.IO;
using FPT.Core.Verification;
using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace FPT.Core.Tests.Verification
{
    public class JarFileVerifier_VerifyShould
    {
        [Theory]
        [ClassData(typeof(ReturnDeserializedResultFromCreatedProcess_Data))]
        public void ReturnDeserializedResultFromCreatedProcess(MockJarFileProcessFactory factory, VerifierResult expected)
        {
            var verifier = new JarFileVerifier(factory);

            var result = verifier.Verify(new Level(), "");

            Assert.Equal(expected, result, new VerifierResultEqualityComparer());
        }
        private class ReturnDeserializedResultFromCreatedProcess_Data : TheoryData<MockJarFileProcessFactory, VerifierResult>
        {
            private int datasetCount = 4;
            private Random random = new Random();
            public ReturnDeserializedResultFromCreatedProcess_Data()
            {
                for (int i = 0; i < datasetCount; i++)
                {
                    var diagnostics = new FPTDiagnostic[random.Next(0, 4)];
                    for (int j = 0; j < diagnostics.Length; j++)
                    {
                        diagnostics[j] = random.NextFPTDiagnostic();
                    }
                    var result = new VerifierResult(random.NextBool(), diagnostics);
                    var stream = new MemoryStream();
                    var writer = new StreamWriter(stream);
                    writer.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                    writer.Flush();
                    stream.Position = 0;
                    var factory = new MockJarFileProcessFactory()
                    {
                        Process = new MockSimplifiedProcess()
                        {
                            StandardOutput = new StreamReader(stream),
                        }
                    };
                    Add(factory, result);
                }
            }
        }
        [Theory]
        [ClassData(typeof(PassCorrectParametersToProcessFactory_Data))]
        public void PassCorrectParametersToProcessFactory(MockJarFileProcessFactory factory, Level level, string user)
        {
            var verifier = new JarFileVerifier(factory);

            verifier.Verify(level, user);

            Assert.Equal(level.GetVerifierFilepath(), factory.JarPath);
            Assert.Equal(level.GetProjectFolder(user), factory.ProjectFolder);
            Assert.Equal(level.GetUserFolder(user), factory.UserFolder);
        }
        private class PassCorrectParametersToProcessFactory_Data : TheoryData<MockJarFileProcessFactory, Level, string>
        {
            private int datasetCount = 4;
            private Random random = new Random();
            public PassCorrectParametersToProcessFactory_Data()
            {
                for (int i = 0; i < 4; i++)
                {
                    Add(
                    new MockJarFileProcessFactory()
                    {
                        Process = new MockSimplifiedProcess()
                        {
                            StandardOutput = new StreamReader(new MemoryStream())
                        }
                    },
                    random.GenerateRandomLevel(),
                    random.RandomString(15));
                }
            }
        }
    }
}
