using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
namespace FPT.Core.Tests.Learning
{
    public class IEnumerableTests
    {
        [Fact]
        public void CanUseLinqSingleLikeDb()
        {
            IEnumerable<string> dbSet = new List<string>() { "one", "two", "aye" };

            var result = dbSet.Single(s => s == "one");
            var e = Assert.ThrowsAny<Exception>(() => dbSet.Single(s => s == "not found"));
            Assert.Equal("one", result);
        }
    }
}
