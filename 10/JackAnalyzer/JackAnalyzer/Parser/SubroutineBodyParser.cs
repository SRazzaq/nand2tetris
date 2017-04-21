using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace JackAnalyzer.Parser
{
    internal class SubroutineBodyParser : Parser
    {
        public SubroutineBodyParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<subroutineBody>");

            WriteToken<SymbolToken>("{");

            new VarDecParser(scanner, writer).Parse();
            new StatementsParser(scanner, writer).Parse();

            WriteToken<SymbolToken>("}");

            WriteLine("</subroutineBody>");
        }
    }
}
