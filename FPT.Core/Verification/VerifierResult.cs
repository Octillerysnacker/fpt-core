using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Verification
{
    public class VerifierResult
    {
        public bool Success { get; }
        public IEnumerable<FPTDiagnostic> Diagnostics { get; }
        public VerifierResult(bool success, IEnumerable<FPTDiagnostic> diagnostics)
        {
            Success = success;
            Diagnostics = diagnostics;
        }
    }
}
