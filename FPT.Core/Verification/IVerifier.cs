using FPT.Core.Levels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Verification
{
    public interface IVerifier
    {
        VerifierResult Verify(Level level, string user);
    }
}
