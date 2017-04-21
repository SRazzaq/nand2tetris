using JackAnalyzer.Parser;
using System;
using System.IO;

namespace JackAnalyzer
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
            var tokenFile = Path.ChangeExtension(jackFile, ".tkn");
            var parserFile = Path.ChangeExtension(jackFile, ".prs");

            using (var reader = new StreamReader(jackFile))
            {
                var scanner = new Scanner(reader);
                WriteTokenFile(scanner, tokenFile);

                scanner.GetEnumerator().Reset();
                WriteParserFile(scanner, parserFile);
            }
        }

        private static void WriteTokenFile(Scanner scanner, string tokenFile)
        {
            using (var writer = new StreamWriter(tokenFile))
            {
                writer.WriteLine("<tokens>");
                foreach (var token in scanner)
                {
                    writer.WriteLine(token);
                }
                writer.WriteLine("</tokens>");
            }
        }

        private static void WriteParserFile(Scanner scanner, string parserFile)
        {
            using (var writer = new StreamWriter(parserFile))
            {
                var parser = new ClassParser(scanner.GetEnumerator(), writer);
                parser.Parse();
            }
        }

        private static bool ValidUsage(string[] args)
        {
            bool valid = (args.Length == 1);
            if (!valid)
            {
                Console.WriteLine("Usage: JackAnalyzer [<Path>][<FileName>.jack] ");
            }
            return valid;
        }
    }
}
