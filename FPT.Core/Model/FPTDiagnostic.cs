using System;
using System.Collections.Generic;
using System.Text;

namespace FPT.Core.Model
{
    public class FPTDiagnostic
    {
        public class FileLocation
        {
            public int Line { get; }
            public int Postition { get; }
            public FileLocation(int line, int position)
            {
                Line = line;
                Postition = position;
            }
        }
        public enum DiagnosticKind { Error, Warning, Info };
        public string Message { get; }
        public DiagnosticKind Kind { get; }
        public FileLocation Start { get; }
        public FileLocation End { get; }
        public FPTDiagnostic(string message, DiagnosticKind kind, FileLocation start, FileLocation end)
        {
            Message = message;
            Kind = kind;
            Start = start;
            End = end;
        }
    }
}
