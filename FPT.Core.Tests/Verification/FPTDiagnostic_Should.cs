using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Newtonsoft.Json;
using FPT.Core.Tests.Extensions;
using FPT.Core.Verification;
using FPT.Core.Equality;
using FPT.Core.Extensions;

namespace FPT.Core.Tests.Verification
{
    public class FPTDiagnostic_Should
    {
        [Theory]
        [ClassData(typeof(BeSerializedAndDeserializedProperly_Data))]
        [ClassData(typeof(NullFileLocations_Data))]
        public void BeSerializedAndDeserializedProperly(FPTDiagnostic diagnostic)
        {
            var serialized = JsonConvert.SerializeObject(diagnostic);

            var deserialized = JsonConvert.DeserializeObject<FPTDiagnostic>(serialized);

            Assert.Equal(diagnostic, deserialized, new FPTDiagnosticEqualityComparer());
        }
        private class BeSerializedAndDeserializedProperly_Data : TheoryData<FPTDiagnostic>
        {
            private int datasetCount = 4;
            private Random random = new Random();
            public BeSerializedAndDeserializedProperly_Data()
            {
                for (int i = 0; i < datasetCount; i++)
                {
                    Add(random.NextFPTDiagnostic());
                }
            }
        }
        private class NullFileLocations_Data : TheoryData<FPTDiagnostic> {
            private Random random = new Random();
            private int minRandomLength = 0, maxRandomLength = 25;
            public NullFileLocations_Data()
            {
                Add(GenerateDiagnosticWithRandomMessageandKind(null, GenerateRandomFileLocation()));
                Add(GenerateDiagnosticWithRandomMessageandKind(GenerateRandomFileLocation(), null));
                Add(GenerateDiagnosticWithRandomMessageandKind(null, null));
            }
            private FPTDiagnostic GenerateDiagnosticWithRandomMessageandKind(FPTDiagnostic.FileLocation start, FPTDiagnostic.FileLocation end)
            {
                return new FPTDiagnostic(
                    random.RandomString(random.Next(minRandomLength, maxRandomLength)),
                    (FPTDiagnostic.DiagnosticKind)random.Next(0, 3),
                    start,
                    end);
            }
            private FPTDiagnostic.FileLocation GenerateRandomFileLocation()
            {
                return new FPTDiagnostic.FileLocation(random.Next(), random.Next());
            }
        }
    }
}
