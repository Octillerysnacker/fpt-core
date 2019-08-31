using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FPT.Core.Tests.IO
{
    public class MockSimplifiedProcess : ISimplifiedProcess
    {
        public StreamReader StandardOutput { get; set; }
        public bool HasBeenWaited { get; private set; }

        public void WaitForExit()
        {
            HasBeenWaited = true;
        }
    }
}
