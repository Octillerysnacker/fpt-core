using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.IO
{
    public class MockJarFileProcessFactory : IJarFileProcessFactory
    {
        public ISimplifiedProcess Process { get; set; }
        public string JarPath { get; private set; }
        public string UserFolder { get; private set; }
        public string ProjectFolder { get; private set; }
        public ISimplifiedProcess CreateProcess(string jarPath, string userFolder, string projectFolder)
        {
            JarPath = jarPath;
            UserFolder = userFolder;
            ProjectFolder = projectFolder;
            return Process;
        }
    }
}
