using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Newtonsoft.Json;
using FPT.Core.Tests.Extensions;
using FPT.Core.Verification;
using FPT.Core.Equality;

namespace FPT.Core.Tests.Verification
{
    public class FPTDiagnostic_Should
    {
        [Theory]
        [ClassData(typeof(BeSerializedAndDeserializedProperly_Data))]
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
    }
}
