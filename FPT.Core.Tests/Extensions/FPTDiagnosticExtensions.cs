using FPT.Core.Extensions;
using FPT.Core.Verification;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.Extensions
{
    public static class FPTDiagnosticExtensions
    {
        public static FPTDiagnostic NextFPTDiagnostic(this Random random)
        {
            return new FPTDiagnostic(
                            random.RandomString(10),
                            (FPTDiagnostic.DiagnosticKind)random.Next(0, 3),
                            new FPTDiagnostic.FileLocation(random.Next(0, 10), random.Next(0, 10)),
                            new FPTDiagnostic.FileLocation(random.Next(0, 10), random.Next(0, 10))
                            );
        }
    }
}
