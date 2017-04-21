using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class LetStatementParser : Parser
    {
        public LetStatementParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<letStatement>");

            WriteToken<KeywordToken>("let");
            WriteToken<IdentifierToken>(); // varName

            if (CurrentToken.Value == "[")
            {
                WriteToken<SymbolToken>("[");
                new ExpressionParser(scanner, writer).Parse();
                WriteToken<SymbolToken>("]");
            }

            WriteToken<SymbolToken>("=");
            new ExpressionParser(scanner, writer).Parse();
            WriteToken<SymbolToken>(";");

            WriteLine("</letStatement>");
        }
    }
}
