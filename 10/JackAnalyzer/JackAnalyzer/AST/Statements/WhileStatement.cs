using JackAnalyzer.Visitor;
using System.Collections.Generic;

namespace JackAnalyzer.AST
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
