using System;
using System.IO;

namespace Assembler
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (!ValidUsage(args)) return;

            var asmFile = FileNameWithExtension(args[0]);
            if (!FileExists(asmFile)) return;

            var hackFile = Path.ChangeExtension(asmFile, "hack");

            SymbolLoader.Load(asmFile);
            CodeAssembler.Assemble(asmFile, hackFile);
        }

        private static bool FileExists(string fileName)
        {
            bool exists = File.Exists(fileName);
            if (!exists)
            {
                Console.WriteLine("File Not Found: {0}", fileName);
            }
            return exists;
        }

        private static bool ValidUsage(string[] args)
        {
            bool valid = (args.Length == 1);
            if (!valid)
            {
                Console.WriteLine("Usage: Assembler file[.asm]");
            }
            return valid;
        }

        private static string FileNameWithExtension(string file)
        {
            if (Path.GetExtension(file) != ".asm") file += ".asm";
            return file;
        }
    }
}