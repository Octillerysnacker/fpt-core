using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.IO
{
    public interface ICopyDir
    {
        void CopyTo(string source, string dest);
    }
}
