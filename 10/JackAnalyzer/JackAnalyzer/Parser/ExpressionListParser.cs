using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class ExpressionListParser : Parser
    {
        public ExpressionListParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<expressionList>");

            if (CurrentToken.Value != ")")
            {
                new ExpressionParser(scanner, writer).Parse();
                while (CurrentToken.Value == ",")
                {
                    WriteToken<SymbolToken>(",");
                    new ExpressionParser(scanner, writer).Parse();
                }
            }

            WriteLine("</expressionList>");
        }
    }
}
