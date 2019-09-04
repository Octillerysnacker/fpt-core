using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using FPT.Core.IO;
using FPT.Core.Levels;

namespace FPT.Core.Verification
{
    public class JarFileVerifier : IVerifier
    {
        private IJarFileProcessFactory factory;

        public JarFileVerifier(IJarFileProcessFactory factory)
        {
            this.factory = factory;
        }

        public VerifierResult Verify(Level level, string user)
        {
            var process = factory.CreateProcess(
                level.GetVerifierFilepath(),
                level.GetUserFolder(user),
                level.GetProjectFolder(user));
            var json = process.StandardOutput.ReadToEnd();
            return JsonConvert.DeserializeObject<VerifierResult>(json);
        }
    }
}
