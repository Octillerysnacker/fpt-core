using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Levels.Providers
{
    public interface ILevelsProvider
    {
        IEnumerable<Level> GetLevels();
        Level GetLevel(string levelId);
    }
}
