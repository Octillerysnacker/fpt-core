using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Model;
namespace FPT.Core
{
    public class LevelEqualityComparer : IEqualityComparer<Level>
    {
        public bool Equals(Level x, Level y)
        {
            return x.Id == y.Id &&
                x.Name == y.Name &&
                x.InitializerFilepath == y.InitializerFilepath &&
                x.VerifierFilepath == y.VerifierFilepath &&
                x.InstructionsFilepath == y.InstructionsFilepath &&
                x.FolderFilepath == y.FolderFilepath;
        }

        public int GetHashCode(Level obj)
        {
            unchecked
            {
                int hash = 29;
                hash = hash * 41 + obj.Name.GetHashCode();
                hash = hash * 41 + obj.Id.GetHashCode();
                hash = hash * 41 + obj.InitializerFilepath.GetHashCode();
                hash = hash * 41 + obj.VerifierFilepath.GetHashCode();
                hash = hash * 41 + obj.InstructionsFilepath.GetHashCode();
                hash = hash * 41 + obj.FolderFilepath.GetHashCode();
                return hash;
            }
        }
    }
}
