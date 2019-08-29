using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.IO;
using FPT.Core.Model;

namespace FPT.Core.Tests.IO
{
    public class CustomFakeLevelsProvider : ILevelsProvider
    {
        public Level Level { get; set; }
        public Level GetLevel(string levelId)
        {
            return Level;
        }

        public IEnumerable<Level> GetLevels()
        {
            return new[] { Level };
        }
    }
}
