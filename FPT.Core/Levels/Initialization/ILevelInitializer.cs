using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Levels.Initialization
{
    public interface ILevelInitializer
    {
        void InitializeIfNecessary(string levelId, string user);
    }
}
