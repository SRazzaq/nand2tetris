using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class ClassVarDecParser : Parser
    {
        public ClassVarDecParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public ClassVarDec Parse()
        {
            var classVarDec = new ClassVarDec();

            classVarDec.Modifier = GetToken<KeywordToken>().Value; // ('static' | 'field')
            classVarDec.Type = GetToken<Token>().Value; // type
            classVarDec.Names.Add(GetToken<IdentifierToken>().Value); // varName 

            while (CurrentToken.Value == ",")
            {
                GetToken<SymbolToken>(",");
                classVarDec.Names.Add(GetToken<IdentifierToken>().Value);
            }

            GetToken<SymbolToken>(";");

            return classVarDec;
        }
    }
}
