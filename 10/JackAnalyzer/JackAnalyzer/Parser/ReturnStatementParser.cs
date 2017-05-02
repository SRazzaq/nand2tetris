using JackAnalyzer.AST;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class ReturnStatementParser : Parser
    {
        public ReturnStatementParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public ReturnStatement Parse()
        {
            var _return = new ReturnStatement();

            GetToken<KeywordToken>("return");
            if (CurrentToken.Value != ";")
            {
                _return.Expression = new ExpressionParser(scanner).Parse();
            }

            GetToken<SymbolToken>(";");

            return _return;
        }
    }
}
