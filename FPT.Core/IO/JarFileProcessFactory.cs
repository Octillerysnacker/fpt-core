using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
namespace FPT.Core.IO
{
    public class JarFileProcessFactory : IJarFileProcessFactory
    {
        public ISimplifiedProcess CreateProcess(string jarPath, string userFolder, string projectFolder)
        {
            var p = new SimplifiedProcess()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "java",
                    Arguments = $"-jar {jarPath} {userFolder} {projectFolder}",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            p.Start();
            return p;
        }
    }
}
