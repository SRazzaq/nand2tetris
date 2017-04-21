using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    internal class VarDecParser : Parser
    {
        public VarDecParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            while (CurrentToken.Value == "var")
            {
                WriteLine("<varDec>");

                WriteToken<KeywordToken>("var");

                // first variable is required.
                WriteToken<Token>(); // type
                WriteToken<IdentifierToken>(); // varName

                while (CurrentToken.Value == ",")
                {
                    WriteToken<SymbolToken>(",");
                    WriteToken<IdentifierToken>(); // varName
                }

                WriteToken<SymbolToken>(";");

                writer.WriteLine("</varDec>");
            }
        }
    }
}
