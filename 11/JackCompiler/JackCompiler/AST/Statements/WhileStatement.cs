using JackCompiler.Visitor;
using System.Collections.Generic;

namespace JackCompiler.AST
{
    public class WhileStatement : Statement
    {
        public WhileStatement()
        {
            this.Statements = new List<Statement>();
        }

        public Expression Condition { get; set; }
        public IList<Statement> Statements { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
