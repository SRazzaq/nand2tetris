using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class ReturnStatementParser : Parser
    {
        public ReturnStatementParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<returnStatement>");

            WriteToken<KeywordToken>("return");
            if (CurrentToken.Value != ";") new ExpressionParser(scanner, writer).Parse();
            WriteToken<SymbolToken>(";");

            WriteLine("</returnStatement>");
        }
    }
}
