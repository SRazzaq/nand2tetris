using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class ClassVarDecParser : Parser
    {
        public ClassVarDecParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<classVarDec>");

            WriteToken<KeywordToken>(); // ('static' | 'field')
            WriteToken<Token>(); // type
            WriteToken<IdentifierToken>(); // varName 

            if (CurrentToken.Value == ",")
            {
                WriteToken<SymbolToken>(",");
                WriteToken<IdentifierToken>();
            }

            WriteToken<SymbolToken>(";");

            writer.WriteLine("</classVarDec>");
        }
    }
}
