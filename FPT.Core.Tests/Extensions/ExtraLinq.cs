using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Core.Tests.Extensions
{
    public static class ExtraLinq
    {
        //Two methods below stolen from https://stackoverflow.com/a/7259419
        public static T RandomElement<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.RandomElementUsing<T>(new Random());
        }

        public static T RandomElementUsing<T>(this IEnumerable<T> enumerable, Random rand)
        {
            int index = rand.Next(0, enumerable.Count());
            return enumerable.ElementAt(index);
        }
    }
}
