using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace FPT.Core.Tests.Learning
{
    public class ProcessTest
    {
        [Fact]
        public void ShouldReadAllTextInProcess()
        {
            var sb = new StringBuilder();

            int totalTime = 5000;
            int linesToWrite = 50;
            for (int i = 0; i < totalTime; i += totalTime / linesToWrite)
            {
                sb.AppendLine($"This is line {i}");
            }
            var expected = sb.ToString();

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo()
                {
                    FileName = "dotnet",
                    Arguments = "\"" + @".\Learning\writerboi\writer boi.dll" + "\"" + " " + "50",
                    UseShellExecute = false,
                    RedirectStandardOutput = true
                }
            };
            process.Start();

            var result = process.StandardOutput.ReadToEnd();

            //process.WaitForExit();

            Assert.Equal(expected, result);
        }
        [Fact]
        /* I frankly have no idea what is going on here. But it be here. */
        public void StreamReaderBehaviorIsNotSpecificToProcess()
        {
            void weirdFunction(Action<string> callback)
            {
                for(int i = 0; i < 50; i++)
                {
                    callback($"Weird ass string {i}");
                }
            }
            using (var stream = new MemoryStream())
            {
                var writer = new StreamWriter(stream) { AutoFlush = true};
                var sb = new StringBuilder();
                weirdFunction(s => sb.AppendLine(s));
                    weirdFunction(s =>
                    {
                        writer.WriteLine(s);
                        //Thread.Sleep(100);
                    });
                stream.Position = 0;
                var reader = new StreamReader(stream);
                var result = reader.ReadToEnd();
                var expected = sb.ToString();
                Assert.Equal(expected, result);
            }
        }
    }
}
