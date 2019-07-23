using FPT.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Tests.IO
{
    //TODO: Name this better
    /// <summary>
    /// A triad is simply a container for a <see cref="FakeLevelsProvider"/>, a <see cref="Model.Level"/>, and a User in the form of a <see cref="string"/>.
    /// Usually, these are used by tests, where <see cref="Level"/> is a randomly selected level in <see cref="Provider"/>.
    /// <para>See <seealso cref="RandomTriadFactory"/> to create such triads.</para>
    /// </summary>
    public class Triad
    {
        public FakeLevelsProvider Provider { get; private set; }
        public Level Level { get; private set; }
        public string User { get; private set; }
        public Triad(FakeLevelsProvider provider, Level level, string user)
        {
            Provider = provider;
            Level = level;
            User = user;
        }
    }
}
