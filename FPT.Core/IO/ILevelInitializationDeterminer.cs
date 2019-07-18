using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.IO
{
    public interface ILevelInitializationDeterminer
    {
        bool RequiresInitialization(string levelId, string user);
    }
}
