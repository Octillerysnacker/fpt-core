using FPT.Core.IO;
using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.IO
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
