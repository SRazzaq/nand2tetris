using System;
using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class StatementsParser : Parser
    {
        public StatementsParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<statements>");

            while (CurrentToken.Value != "}")
            {
                switch (CurrentToken.Value)
                {
                    case "let":
                        new LetStatementParser(scanner, writer).Parse();
                        break;
                    case "if":
                        new IfStatementParser(scanner, writer).Parse();
                        break;
                    case "while":
                        new WhileStatementParser(scanner, writer).Parse();
                        break;
                    case "do":
                        new DoStatementParser(scanner, writer).Parse();
                        break;
                    case "return":
                        new ReturnStatementParser(scanner, writer).Parse();
                        break;
                }
            }

            WriteLine("</statements>");
        }
    }
}
