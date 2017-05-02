using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class SubroutineBodyParser : Parser
    {
        public SubroutineBodyParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public SubroutineBody Parse()
        {
            var subroutineBody = new SubroutineBody();

            GetToken<SymbolToken>("{");

            while (CurrentToken.Value == "var")
            {

                subroutineBody.VarDecs.Add(new VarDecParser(scanner).Parse());
            }

            while (CurrentToken.Value != "}")
            {
                subroutineBody.Statements.Add(new StatementParser(scanner).Parse());
            }

            GetToken<SymbolToken>("}");

            return subroutineBody;
        }
    }
}
