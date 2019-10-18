using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Levels.Initialization
{
    public interface ILevelInitializationDeterminer
    {
        bool RequiresInitialization(string userFolderPath);
        void MarkAsInitialized(string userFolderPath);
        void UnmarkAsInitialized(string userFOlderPath);
    }
}
