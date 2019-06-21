using FPT.Core.IO;
using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.IO
{
    public class FakeFPTContext : IFPTContext
    {
        public IEnumerable<Level> Levels { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
