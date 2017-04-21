using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class DoStatementParser : Parser
    {
        public DoStatementParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<doStatement>");

            WriteToken<KeywordToken>("do");
            WriteToken<Token>();
            switch (CurrentToken.Value)
            {
                case "(":
                    WriteToken<SymbolToken>("(");
                    new ExpressionListParser(scanner, writer).Parse();
                    WriteToken<SymbolToken>(")");
                    break;
                case ".":
                    WriteToken<SymbolToken>(".");
                    WriteToken<Token>();
                    WriteToken<SymbolToken>("(");
                    new ExpressionListParser(scanner, writer).Parse();
                    WriteToken<SymbolToken>(")");
                    break;
            }
            WriteToken<SymbolToken>(";");

            WriteLine("</doStatement>");
        }
    }
}
