using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using System.Text;
using System.IO;
namespace FPT.Core.IO
{
    //Literally copied straight from MSDN, but with modifications to accomodate IO abstractions
    public class CopyDir : ICopyDir
    {
        private IDirectory directory;
        private IPath path;
        private IFileSystem fileSystem;
        public CopyDir(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
            path = fileSystem.Path;
            directory = fileSystem.Directory;
        }
        public void CopyAll(string source, string target)
        {
            var directoryInfoFactory = fileSystem.DirectoryInfo;
            var sourceDirectoryInfo = directoryInfoFactory.FromDirectoryName(source);
            var targetDirectoryInfo = directoryInfoFactory.FromDirectoryName(target);
            CopyAll(sourceDirectoryInfo,targetDirectoryInfo);
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
