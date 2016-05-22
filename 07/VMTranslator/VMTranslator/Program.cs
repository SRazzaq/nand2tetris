using System;
using System.IO;

namespace VMTranslator
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

        private static void ProcessFile(string file)
        {
            var asmFile = Path.ChangeExtension(file, ".asm");
            using (var reader = new StreamReader(file))
            {
                using (var writer = new StreamWriter(asmFile))
                {

                    foreach (var command in new Parser(reader))
                    {
                        writer.WriteLine(command.Code);
                        Console.Write(command.Code);
                    }
                }
            }

        }

        private static void ProcessDirectory(string dir)
        {
            var asmFile = Path.Combine(dir, new DirectoryInfo(dir).Name + ".asm");
            var writer = new CodeWriter(asmFile);
            foreach (var vm in Directory.GetFiles(dir, "*.vm"))
            {
                // Parser = new Parser()
                // Process vm
            }
        }

        private static bool ValidUsage(string[] args)
        {
            bool valid = (args.Length == 1);
            if (!valid)
            {
                Console.WriteLine("Usage: VMTranslator [<Path>][<FileName>.vm] ");
            }
            return valid;
        }

    }
}
