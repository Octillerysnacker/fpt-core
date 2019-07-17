using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;

namespace FPT.Core.IO
{
    //Literally copied straight from MSDN, but with modifications to accomodate IO abstractions
    public class CopyDir
    {
        private IDirectory directory;
        private IPath path;
        public CopyDir(IDirectory directory, IPath path)
        {
            this.directory = directory;
            this.path = path;
        }
        public void CopyAll(IDirectoryInfo source, IDirectoryInfo target)
        {
            directory.CreateDirectory(target.FullName);

            // Copy each file into the new directory.
            foreach (IFileInfo fi in source.GetFiles())
            {
                fi.CopyTo(path.Combine(target.FullName, fi.Name), true);
            }

            // Copy each subdirectory using recursion.
            foreach (IDirectoryInfo diSourceSubDir in source.GetDirectories())
            {
                IDirectoryInfo nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir);
            }
        }
    }
}
