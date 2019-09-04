using FPT.Core.Commands;
using FPT.Core.IO;
using System;
using System.IO.Abstractions;
using Newtonsoft.Json;
using FPT.Core.Levels.Initialization;
using FPT.Core.Levels.Providers;

namespace FPT.Core.MainApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var main = MainAppFactory.MakeApp();
            try
            {
                var result = main.Execute(args);
                Console.WriteLine(JsonConvert.SerializeObject(result));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(JsonConvert.SerializeObject(new SummarizedException(e)));
            }
        }
        private class SummarizedException
        {
            public Type Type { get; }
            public string Message { get; }
            public SummarizedException InnerException { get; }
            public SummarizedException(Exception e)
            {
                Type = e.GetType();
                Message = e.Message;
                if (!(e.InnerException is null))
                {
                    InnerException = new SummarizedException(e.InnerException);
                }
            }
        }
    }
}
