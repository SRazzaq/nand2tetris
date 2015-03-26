using System.IO;

namespace Assembler
{
    internal class CodeAssembler
    {
        internal static void Assemble(string asmFile, string hackFile)
        {
            using (var reader = new StreamReader(asmFile))
            {
                using (var writer = new StreamWriter(hackFile))
                {
                    foreach (var command in new Parser(reader))
                    {
                        if (!(command is LCommand))
                        {
                            writer.WriteLine(command.Code);
                        }
                    }
                }
            }
        }
    }
}
