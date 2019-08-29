using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.IO
{
    public interface IVerifier
    {
        VerifierResult Verify(Level level, string user);
    }
}
