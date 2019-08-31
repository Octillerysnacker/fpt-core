using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace FPT.Core
{
    public class VerifierResultEqualityComparer : IEqualityComparer<VerifierResult>
    {
        private FPTDiagnosticEqualityComparer fComparer = new FPTDiagnosticEqualityComparer();
        public bool Equals(VerifierResult x, VerifierResult y)
        {
            return x.Success == y.Success &&
                x.Diagnostics.SequenceEqual(y.Diagnostics, fComparer);
        }

        public int GetHashCode(VerifierResult obj)
        {
            unchecked {
                int hash = 51;
                hash = hash * 71 + obj.Diagnostics.GetHashCode();
                hash = hash * 71 + obj.Success.GetHashCode();
                return hash;
            }
        }
    }
}
