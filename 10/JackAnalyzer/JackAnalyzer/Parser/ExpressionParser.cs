using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class ExpressionParser : Parser
    {
        public ExpressionParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public Expression Parse()
        {
            var expression = new Expression();

            expression.Term = new TermParser(scanner).Parse();
            while (CurrentToken.Value == "+" || CurrentToken.Value == "-" ||
                   CurrentToken.Value == "*" || CurrentToken.Value == "/" ||
                   CurrentToken.Value == "&" || CurrentToken.Value == "|" ||
                   CurrentToken.Value == "<" || CurrentToken.Value == ">" ||
                   CurrentToken.Value == "=")
            {
                expression.Operator = GetToken<SymbolToken>().Value;
                expression.OtherTerm = new TermParser(scanner).Parse();
            }

            return expression;
        }
    }
}
