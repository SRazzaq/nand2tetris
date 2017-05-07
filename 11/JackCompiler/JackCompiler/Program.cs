using JackCompiler.Parser;
using JackCompiler.Visitor;
using System;
using System.IO;

namespace JackCompiler
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!ValidUsage(args)) return;

            var source = args[0];

            if (IsDirectory(source))
            {
                ProcessDirectory(source);
            }
            else
            {
                ProcessFile(source);
            }
        }

        private static bool IsDirectory(string source)
        {
            return (File.GetAttributes(source) & FileAttributes.Directory) == FileAttributes.Directory;
        }

        private static void ProcessDirectory(string dir)
        {
            foreach (var jackFile in Directory.GetFiles(dir, "*.jack"))
            {
                ProcessFile(jackFile);
            }
        }

        private static void ProcessFile(string jackFile)
        {
            GenerateOutput(jackFile);
        }

        private static void GenerateOutput(string jackFile)
        {
            var vmFile = Path.ChangeExtension(jackFile, ".vm");

            using (var reader = new StreamReader(jackFile))
            {
                var tokenizer = new Tokenizer(reader);
                WriteVMFile(tokenizer, vmFile);
            }
        }

        private static void WriteVMFile(Tokenizer scanner, string parserFile)
        {
            var _class = new ClassParser(scanner.GetEnumerator()).Parse();

            using (var writer = new StreamWriter(parserFile))
            {
                var vmVisitor = new VMVisitor(writer);
                vmVisitor.Visit(_class);
            }
        }

        private static bool ValidUsage(string[] args)
        {
            bool valid = (args.Length == 1);
            if (!valid)
            {
                Console.WriteLine("Usage: JackCompiler [<Path>][<FileName>.jack] ");
            }
            return valid;
        }
    }
}
