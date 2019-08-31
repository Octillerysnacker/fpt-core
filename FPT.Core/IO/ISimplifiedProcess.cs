using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FPT.Core.IO
{
    public interface ISimplifiedProcess
    {
        StreamReader StandardOutput { get; }
    }
}
