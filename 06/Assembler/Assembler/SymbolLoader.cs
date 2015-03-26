using System.IO;

namespace Assembler
{
    internal static class SymbolLoader
    {
        internal static void Load(string asmFile)
        {
            using (var reader = new StreamReader(asmFile))
            {
                int lineCount = 0;
                foreach (var command in new Parser(reader))
                {
                    if (command is LCommand)
                    {
                        var symbol = command.Mnemonic.TrimStart('(').TrimEnd(')');
                        SymbolTable.Add(symbol, lineCount);
                    }
                    else
                    {
                        lineCount++;
                    }
                }
            }
        }
    }
}
