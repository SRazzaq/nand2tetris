using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class DoStatementParser : Parser
    {
        public DoStatementParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public DoStatement Parse()
        {
            var doStatement = new DoStatement();

            GetToken<KeywordToken>("do");

            var token = GetToken<Token>();
            if (CurrentToken.Value == ".")
            {
                doStatement.ClassName = token.Value;
                GetToken<SymbolToken>(".");
                doStatement.SubroutineName = GetToken<Token>().Value;
            }
            else
            {
                doStatement.SubroutineName = token.Value;
            }

            GetToken<SymbolToken>("(");
            while (CurrentToken.Value != ")")
            {
                doStatement.Expressions.Add(new ExpressionParser(scanner).Parse());
                if (CurrentToken.Value == ",") GetToken<SymbolToken>(",");
            }
            GetToken<SymbolToken>(")");

            GetToken<SymbolToken>(";");

            return doStatement;
        }
    }
}
