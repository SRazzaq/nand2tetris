using JackCompiler.AST;
using System.Collections.Generic;

namespace JackCompiler.Parser
{
    internal class IfStatementParser : Parser
    {
        public IfStatementParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public IfStatement Parse()
        {
            var ifStatement = new IfStatement();

            GetToken<KeywordToken>("if");
            GetToken<SymbolToken>("(");
            ifStatement.Condition = new ExpressionParser(scanner).Parse();
            GetToken<SymbolToken>(")");

            GetToken<SymbolToken>("{");
            while (CurrentToken.Value != "}")
            {
                ifStatement.IfStatements.Add(new StatementParser(scanner).Parse());
            }
            GetToken<SymbolToken>("}");

            if (CurrentToken.Value == "else")
            {
                GetToken<KeywordToken>("else");
                GetToken<SymbolToken>("{");
                while (CurrentToken.Value != "}")
                {
                    ifStatement.ElseStatements.Add(new StatementParser(scanner).Parse());
                }
                GetToken<SymbolToken>("}");
            }

            return ifStatement;
        }
    }
}
