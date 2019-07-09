using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.IO
{
    public interface ILevelsProvider
    {
        IEnumerable<Level> GetLevels();
    }
}
