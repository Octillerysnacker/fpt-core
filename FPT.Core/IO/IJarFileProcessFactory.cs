using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.IO
{
    public interface IJarFileProcessFactory
    {
        ISimplifiedProcess CreateProcess(string jarPath, string userFolder, string projectFolder);
    }
}
