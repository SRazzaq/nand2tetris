using System.Collections.Generic;
using System.IO;

namespace JackAnalyzer.Parser
{
    public class ParameterListParser : Parser
    {
        public ParameterListParser(IEnumerator<Token> scanner, StreamWriter writer)
            : base(scanner, writer)
        {
        }

        public override void Parse()
        {
            WriteLine("<parameterList>");

            if (CurrentToken.Value != ")")
            {
                // First parameter is required.
                WriteToken<Token>(); //type
                WriteToken<IdentifierToken>(); //varName

                while (CurrentToken.Value != ")")
                {
                    WriteToken<SymbolToken>(",");

                    WriteToken<Token>(); //type
                    WriteToken<IdentifierToken>(); //varName
                }
            }

            WriteLine("</parameterList>");
        }
    }
}
