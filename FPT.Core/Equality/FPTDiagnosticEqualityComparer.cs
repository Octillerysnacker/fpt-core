using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Verification;

namespace FPT.Core.Equality
{
    public class FPTDiagnosticEqualityComparer : IEqualityComparer<FPTDiagnostic>
    {
        public bool Equals(FPTDiagnostic x, FPTDiagnostic y)
        {
            return x.End.Line == y.End.Line &&
                x.End.Position == y.End.Position &&
                x.Kind == y.Kind &&
                x.Message == y.Message &&
                x.Start.Line == y.Start.Line &&
                x.Start.Position == y.Start.Position;
        }

        public int GetHashCode(FPTDiagnostic obj)
        {
            unchecked
            {
                int hash = 107;
                hash = hash * 23 + obj.End.Line.GetHashCode();
                hash = hash * 23 + obj.End.Position.GetHashCode();
                hash = hash * 23 + obj.Kind.GetHashCode();
                hash = hash * 23 + obj.Message.GetHashCode();
                hash = hash * 23 + obj.Start.Line.GetHashCode();
                hash = hash * 23 + obj.Start.Position.GetHashCode();
                return hash;
            }
        }
    }
}
