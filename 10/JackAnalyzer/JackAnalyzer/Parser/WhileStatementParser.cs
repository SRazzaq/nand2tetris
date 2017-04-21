using System;
using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class WhileStatementParser : Parser
    {
        public WhileStatementParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<whileStatement>");

            WriteToken<KeywordToken>("while");
            WriteToken<SymbolToken>("(");
            new ExpressionParser(scanner, writer).Parse();
            WriteToken<SymbolToken>(")");

            WriteToken<SymbolToken>("{");
            new StatementsParser(scanner, writer).Parse();
            WriteToken<SymbolToken>("}");

            WriteLine("</whileStatement>");
        }
    }
}
