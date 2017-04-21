using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class ExpressionParser : Parser
    {
        public ExpressionParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<expression>");

            new TermParser(scanner, writer).Parse();
            while (CurrentToken.Value == "+" || CurrentToken.Value == "-" ||
                   CurrentToken.Value == "*" || CurrentToken.Value == "/" ||
                   CurrentToken.Value == "&" || CurrentToken.Value == "|" ||
                   CurrentToken.Value == "<" || CurrentToken.Value == ">" ||
                   CurrentToken.Value == "=")
            {
                WriteToken<SymbolToken>();
                new TermParser(scanner, writer).Parse();
            }

            WriteLine("</expression>");
        }
    }
}
