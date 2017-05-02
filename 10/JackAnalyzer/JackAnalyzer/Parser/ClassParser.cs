using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class ClassParser : Parser
    {
        public ClassParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public Class Parse()
        {
            var _class = new Class();

            GetToken<KeywordToken>("class");
            _class.Name = GetToken<IdentifierToken>().Value; // className
            GetToken<SymbolToken>("{");

            while (CurrentToken.Value == "static" || CurrentToken.Value == "field")
            {
                _class.ClassVarDecs.Add(new ClassVarDecParser(scanner).Parse());
            }

            while (CurrentToken.Value == "constructor" || CurrentToken.Value == "function" || CurrentToken.Value == "method")
            {
                _class.SubrouteDecs.Add(new SubrouteDecParser(scanner).Parse());
            }

            GetToken<SymbolToken>("}");

            return _class;
        }
    }
}
