using JackCompiler.Visitor;
using System.Collections.Generic;

namespace JackCompiler.AST
{
    public class IfStatement : Statement
    {
        public IfStatement()
        {
            IfStatements = new List<Statement>();
            ElseStatements = new List<Statement>();
        }

        public Expression Condition { get; set; }

        public IList<Statement> IfStatements { get; private set; }
        public IList<Statement> ElseStatements { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
