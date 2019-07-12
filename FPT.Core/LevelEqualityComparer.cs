using System;
using System.Collections.Generic;
using System.Text;
using FPT.Core.Model;
namespace FPT.Core
{
    //TO-DO: Veriy equality comparer is correctly implemented
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
                return obj.Name.GetHashCode() * 3 +
                    obj.Id.GetHashCode() * 5 +
                    obj.InitializerFilepath.GetHashCode() * 7 +
                    obj.VerifierFilepath.GetHashCode() * 11 +
                    obj.InstructionsFilepath.GetHashCode() * 13 +
                    obj.FolderFilepath.GetHashCode() * 17;
            }
        }
    }
}
