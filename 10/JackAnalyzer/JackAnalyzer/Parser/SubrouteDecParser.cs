using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class SubrouteDecParser : Parser
    {
        public SubrouteDecParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<subroutineDec>");

            WriteToken<KeywordToken>();// ('constructor' | 'function' | 'method')
            WriteToken<Token>(); // ('void' | type)
            WriteToken<IdentifierToken>(); // subroutineName 

            WriteToken<SymbolToken>("(");

            new ParameterListParser(scanner, writer).Parse();

            WriteToken<SymbolToken>(")");

            new SubroutineBodyParser(scanner, writer).Parse();

            WriteLine("</subroutineDec>");
        }
    }
}
