using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
{
    public class LetStatement : Statement
    {
        public string Variable { get; set; }
        public Expression ArrayExpression { get; set; }

        public Expression Expression { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
