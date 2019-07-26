using FPT.Core.IO;
using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Commands
{
    public class GetCoursesCommand : IExecutable
    {
        private IFPTContext context;
        public GetCoursesCommand(string commandId, IFPTContext context) : base(commandId)
        {
            this.context = context;
        }

        public override object Execute(params string[] args)
        {
            return context.Courses;
        }
    }
}
