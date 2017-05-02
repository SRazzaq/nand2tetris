using JackAnalyzer.Visitor;
using System.Collections.Generic;

namespace JackAnalyzer.AST
{
    public class DoStatement : Statement
    {
        public DoStatement()
        {
            this.Expressions = new List<Expression>();
        }

        public string ClassName { get; set; }
        public string SubroutineName { get; set; }
        public IList<Expression> Expressions { get; private set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
