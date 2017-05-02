using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class SubrouteDecParser : Parser
    {
        public SubrouteDecParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public SubrouteDec Parse()
        {
            var subrouteDec = new SubrouteDec();

            subrouteDec.Type = GetToken<KeywordToken>().Value;// ('constructor' | 'function' | 'method')
            subrouteDec.ReturnType = GetToken<Token>().Value; // ('void' | type)
            subrouteDec.Name = GetToken<IdentifierToken>().Value; // subroutineName 

            GetToken<SymbolToken>("(");
            while (CurrentToken.Value != ")")
            {
                if (CurrentToken.Value == ",") GetToken<SymbolToken>(",");
                subrouteDec.Parameters.Add(new ParameterParser(scanner).Parse());
            }
            GetToken<SymbolToken>(")");

            subrouteDec.SubroutineBody = new SubroutineBodyParser(scanner).Parse();

            return subrouteDec;
        }
    }
}
