using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Extensions
{
    public static class RandomExtensions
    {
        private static readonly string alphanumericCharacters = "qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM";
        public static string RandomString(this Random random, int length)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                stringBuilder.Append(alphanumericCharacters[random.Next(alphanumericCharacters.Length)]);
            }
            return stringBuilder.ToString();
        }
        public static bool NextBool(this Random random)
        {
            return (random.Next(0, 2) == 0) ? false : true;
        }
    }
}
