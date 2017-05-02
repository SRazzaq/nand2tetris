using JackAnalyzer.Visitor;

namespace JackAnalyzer.AST
{
    public class ExpressionTerm : Term
    {
        public Expression Expression { get; set; }

        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
