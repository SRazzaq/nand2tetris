using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class VarDecParser : Parser
    {
        public VarDecParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public VarDec Parse()
        {
            var varDec = new VarDec();

            GetToken<KeywordToken>("var");

            // first variable is required.
            varDec.Type = GetToken<Token>().Value; // type
            varDec.Names.Add(GetToken<IdentifierToken>().Value); // varName

            while (CurrentToken.Value == ",")
            {
                GetToken<SymbolToken>(",");
                varDec.Names.Add(GetToken<IdentifierToken>().Value); // varName
            }

            GetToken<SymbolToken>(";");

            return varDec;
        }
    }
}
