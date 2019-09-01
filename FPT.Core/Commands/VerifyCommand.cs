using FPT.Core.Exceptions;
using FPT.Core.Levels.Providers;
using FPT.Core.Verification;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class VerifyCommand : IExecutable
    {
        private ILevelsProvider provider;
        private IVerifier verifier;
        public VerifyCommand(ILevelsProvider provider, IVerifier verifier)
        {
            this.provider = provider;
            this.verifier = verifier;
        }
        public object Execute(params string[] args)
        {
            if(args is null)
            {
                ThrowNotEnoughParams(0);
            }else if(args.Length < 2){
                ThrowNotEnoughParams(args.Length);
            }
            var levelId = args[0];
            var user = args[1];
            var level = provider.GetLevel(levelId);
            return verifier.Verify(level, user);
        }
        private void ThrowNotEnoughParams(int missingParamCount)
        {
            throw new InvalidCommandArrayException($"Not enough parameters were passed ({missingParamCount} out of 2).");
        }
    }
}
