using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.IO;
namespace FPT.Core.Tests.IO
{
    public class MockCopyDir : ICopyDir
    {
        public bool DidCopy { get; private set; }
        public string SuppliedSourceDir { get; private set; }
        public string SuppliedDestDir { get; private set; }

        public void CopyAll(string source, string dest)
        {
            DidCopy = true;
            SuppliedSourceDir = source;
            SuppliedDestDir = dest;
        }
    }
}
