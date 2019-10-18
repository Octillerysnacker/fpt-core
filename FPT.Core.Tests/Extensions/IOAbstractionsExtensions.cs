using FPT.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using System.Text;

namespace FPT.Core.Tests.Extensions
{
    public static class IOAbstractionsExtensions
    {
        public static MockFileData NextMockFileData(this Random random, int minContentLength, int maxContentLength)
        {
            return new MockFileData(random.RandomString(random.Next(minContentLength, maxContentLength)));
        }
    }
}
