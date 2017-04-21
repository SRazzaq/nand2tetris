using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class IfStatementParser : Parser
    {
        public IfStatementParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<ifStatement>");

            WriteToken<KeywordToken>("if");
            WriteToken<SymbolToken>("(");
            new ExpressionParser(scanner, writer).Parse();
            WriteToken<SymbolToken>(")");

            WriteToken<SymbolToken>("{");
            new StatementsParser(scanner, writer).Parse();
            WriteToken<SymbolToken>("}");

            if (CurrentToken.Value == "else")
            {
                WriteToken<KeywordToken>("else");
                WriteToken<SymbolToken>("{");
                new StatementsParser(scanner, writer).Parse();
                WriteToken<SymbolToken>("}");
            }

            WriteLine("</ifStatement>");
        }
    }
}
