using System;

namespace DotNet.ProjectGraph.Tool.Services
{
    public class ConsoleService : IConsoleService
    {
        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteInfo(string value)
        {
            WriteLine(value);
        }

        public void WriteInput(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteSuccess(string value)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void WriteSample(string value)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void WriteError(string value)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            WriteLine(value);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void WriteLine(string value)
        {
            Console.WriteLine();
            Console.WriteLine(value);
        }
    }
}