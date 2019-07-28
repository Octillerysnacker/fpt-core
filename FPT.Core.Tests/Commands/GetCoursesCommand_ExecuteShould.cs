using FPT.Core.Commands;
using FPT.Core.Model;
using FPT.Core.Tests.IO;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
namespace FPT.Core.Tests.Commands
{
    public class GetCoursesCommand_ExecuteShould
    {
        [Fact]
        public void ReturnAllAvailableCourses()
        {
            var context = new FakeFPTContext();
            var courses = new Course[] { new Course(), new Course(), new Course() };
            context.Courses = courses;
            var command = new GetCoursesCommand(context);

            var result = (Course[])command.Execute();

            Assert.Equal(courses, result);
        }
    }
}
