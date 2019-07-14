using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.IO
{
    public interface ILevelInitializer
    {
        void InitializeIfNecessary(string levelId, string user);
    }
}
