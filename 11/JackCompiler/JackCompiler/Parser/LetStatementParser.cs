using JackCompiler.AST;
using System.Collections.Generic;

namespace JackCompiler.Parser
{
    internal class LetStatementParser : Parser
    {
        public LetStatementParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public LetStatement Parse()
        {
            var letStatement = new LetStatement();

            GetToken<KeywordToken>("let");
            letStatement.Variable = GetToken<IdentifierToken>().Value; // varName

            if (CurrentToken.Value == "[")
            {
                GetToken<SymbolToken>("[");
                letStatement.ArrayExpression = new ExpressionParser(scanner).Parse();
                GetToken<SymbolToken>("]");
            }

            GetToken<SymbolToken>("=");
            letStatement.Expression = new ExpressionParser(scanner).Parse();
            GetToken<SymbolToken>(";");

            return letStatement;
        }
    }
}
