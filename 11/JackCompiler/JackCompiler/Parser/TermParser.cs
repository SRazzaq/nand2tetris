using JackCompiler.AST;
using System;
using System.Collections.Generic;

namespace JackCompiler.Parser
{
    internal class TermParser : Parser
    {
        public TermParser(IEnumerator<Token> scanner)
            : base(scanner)
        {
        }


        public Term Parse()
        {
            if (CurrentToken.Value == "-" || CurrentToken.Value == "~") // Unary operators
            {
                var term = new UnaryTerm();

                term.Operator = GetToken<SymbolToken>().Value;
                term.Term = new TermParser(scanner).Parse();
                return term;
            }

            if (CurrentToken.Value == "(") // Expression
            {
                var term = new ExpressionTerm();

                GetToken<SymbolToken>("(");
                term.Expression = new ExpressionParser(scanner).Parse();
                GetToken<SymbolToken>(")");

                return term;
            }

            var token = GetToken<Token>();

            if (CurrentToken.Value == "(" || CurrentToken.Value == ".") // Subroutine call
            {
                var term = new SubroutineCallTerm();
                if (CurrentToken.Value == ".")
                {
                    term.ClassName = token.Value;
                    GetToken<SymbolToken>(".");
                    term.SubroutineName = GetToken<Token>().Value;
                }
                else
                {
                    term.SubroutineName = token.Value;
                }

                GetToken<SymbolToken>("(");
                while (CurrentToken.Value != ")")
                {
                    term.Expressions.Add(new ExpressionParser(scanner).Parse());
                    if (CurrentToken.Value == ",") GetToken<SymbolToken>(",");
                }
                GetToken<SymbolToken>(")");

                return term;
            }

            if (token is IntegerToken)
            {
                var term = new IntegerConstantTerm();
                term.Value = int.Parse(token.Value);
                return term;
            }

            if (token is StringToken)
            {
                var term = new StringConstantTerm();
                term.Value = token.Value;
                return term;
            }

            if (token is IdentifierToken)
            {
                var term = new VariableTerm();
                term.Variable = token.Value;

                if (CurrentToken.Value == "[") // Array Expression
                {
                    GetToken<SymbolToken>("[");
                    term.ArrayExpression = new ExpressionParser(scanner).Parse();
                    GetToken<SymbolToken>("]");
                }

                return term;
            }

            if (token is KeywordToken)
            {
                var term = new KeywordConstantTerm();
                term.Value = token.Value;
                return term;
            }

            throw new Exception(string.Format("Invalid token found."));
        }
    }
}
