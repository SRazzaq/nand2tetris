using JackCompiler.AST;
using System;
using System.Collections.Generic;
using System.IO;

namespace JackCompiler.Parser
{
    internal class WhileStatementParser : Parser
    {
        public WhileStatementParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public WhileStatement Parse()
        {
            var whileStatement = new WhileStatement();

            GetToken<KeywordToken>("while");
            GetToken<SymbolToken>("(");
            whileStatement.Condition = new ExpressionParser(scanner).Parse();
            GetToken<SymbolToken>(")");

            GetToken<SymbolToken>("{");
            while (CurrentToken.Value != "}")
            {
                whileStatement.Statements.Add(new StatementParser(scanner).Parse());
            }
            GetToken<SymbolToken>("}");

            return whileStatement;
        }
    }
}
