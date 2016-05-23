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

        private static void ProcessFile(string vmFile)
        {
            var asmFile = Path.ChangeExtension(vmFile, ".asm");
            WriteInit(asmFile);
            GenerateAsm(vmFile, asmFile);

        }

        private static void ProcessDirectory(string dir)
        {
            var asmFile = Path.Combine(dir, new DirectoryInfo(dir).Name + ".asm");
            WriteInit(asmFile);
            foreach (var vmFile in Directory.GetFiles(dir, "*.vm"))
            {
                GenerateAsm(vmFile, asmFile);
            }
        }

        private static void WriteInit(string asmFile)
        {
            using (var writer = new StreamWriter(asmFile, false))
            {
                writer.Write(
                    "@256" + Environment.NewLine +
                    "D=A" + Environment.NewLine +
                    "@SP" + Environment.NewLine +
                    "M=D" + Environment.NewLine +
                    new Call("Sys.init", "0").Code + Environment.NewLine
                    );
            }
        }

        private static void GenerateAsm(string vmFile, string asmFile)
        {
            using (var reader = new StreamReader(vmFile))
            {
                using (var writer = new StreamWriter(asmFile, true))
                {
                    foreach (var command in new Parser(reader))
                    {
                        writer.Write(command.Code);
                    }
                }
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
