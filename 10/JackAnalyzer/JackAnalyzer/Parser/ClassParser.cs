using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class ClassParser : Parser
    {
        public ClassParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<class>");

            WriteToken<KeywordToken>("class");
            WriteToken<IdentifierToken>(); // className
            WriteToken<SymbolToken>("{");

            while (CurrentToken.Value == "static" || CurrentToken.Value == "field")
            {
                new ClassVarDecParser(scanner, writer).Parse();
            }

            while (CurrentToken.Value == "constructor" || CurrentToken.Value == "function" || CurrentToken.Value == "method")
            {
                new SubrouteDecParser(scanner, writer).Parse();
            }

            WriteToken<SymbolToken>("}");

            WriteLine("</class>");
        }
    }
}
