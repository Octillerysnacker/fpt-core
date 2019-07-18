using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace FPT.Core.Tests.IO
{
    public class RandomPathsDataSet : TheoryData<string>
    {
        private int numberOfDataSets = 4;
        public RandomPathsDataSet()
        {
            for (int i = 0; i < numberOfDataSets; i++)
            {
                Add(Path.Combine("c:", Path.GetRandomFileName()));
            }
        }
    }
}
