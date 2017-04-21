using System;
using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class TermParser : Parser
    {
        public TermParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }


        public override void Parse()
        {
            WriteLine("<term>");

            switch (CurrentToken.Value)
            {
                case "(":
                    WriteToken<SymbolToken>("(");
                    new ExpressionParser(scanner, writer).Parse();
                    WriteToken<SymbolToken>(")");
                    break;
                case "-":
                    WriteToken<SymbolToken>("-");
                    new TermParser(scanner, writer).Parse();
                    break;
                case "~":
                    WriteToken<SymbolToken>("~");
                    new TermParser(scanner, writer).Parse();
                    break;
                default:
                    WriteToken<Token>();
                    break;
            }

            switch (CurrentToken.Value)
            {
                case "[":
                    WriteToken<SymbolToken>("[");
                    new ExpressionParser(scanner, writer).Parse();
                    WriteToken<SymbolToken>("]");
                    break;
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

            WriteLine("</term>");
        }
    }
}
