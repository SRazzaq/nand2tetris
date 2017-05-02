using JackAnalyzer.AST;
using System;
using System.Collections.Generic;

namespace JackAnalyzer.Parser
{
    internal class StatementParser : Parser
    {
        public StatementParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }

        public Statement Parse()
        {
            switch (CurrentToken.Value)
            {
                case "let":
                    return new LetStatementParser(scanner).Parse();

                case "if":
                    return new IfStatementParser(scanner).Parse();

                case "while":
                    return new WhileStatementParser(scanner).Parse();

                case "do":
                    return new DoStatementParser(scanner).Parse();

                case "return":
                    return new ReturnStatementParser(scanner).Parse();

                default:
                    throw new Exception(string.Format("Invalid token found."));
            }
        }
    }
}
