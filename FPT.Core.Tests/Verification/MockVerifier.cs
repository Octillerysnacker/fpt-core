using FPT.Core.Levels;
using FPT.Core.Verification;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.Verification
{
    public class MockVerifier : IVerifier
    {
        public Level Level { get; private set; }
        public string User { get; private set; }
        public VerifierResult Return { get; set; }
        public VerifierResult Verify(Level level, string user)
        {
            Level = level;
            User = user;
            return Return;
        }
    }
}
